using DevExpress.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Services;

namespace TestPerson.Controllers
{
    public class HomeController : Controller
    {
        public Int32 IdP { get; set; }
        public ActionResult Index()
        {
            List<TestPerson.Models.Person> ListaPersonas = new List<Models.Person>();
            using (TestEntities context = new TestEntities())
            {
                var person = (from r in context.Personas select r);
                foreach (Personas record in person)
                {
                    Models.Person p = new Models.Person { Id= record.Id, Nombre = record.Nombre, FechaNac = record.FechaNacimiento.ToShortDateString(), Pic = (Byte[]) record.Foto };
                    ListaPersonas.Add(p);
                }
            }
            return View("Index", ListaPersonas);
        }
        
        public ActionResult Captura(Int32 IdPersona)
        {
            TestPerson.Models.Person PersonEdit = null;
            if(IdPersona != 0)
            {
                TestEntities context = new TestEntities();
                var person = from r in context.Personas
                             where r.Id == IdPersona
                             select r;
                foreach (Personas p in person)
                {
                    PersonEdit = new Models.Person { Id = p.Id, Nombre = p.Nombre, FechaNac = p.FechaNacimiento.ToShortDateString(), Pic = p.Foto };
                }
                return View(PersonEdit);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Captura(TestPerson.Models.Person P, HttpPostedFileBase Foto)
        {
            try
            {
                TestEntities context = new TestEntities();
                Byte[] bytes = null;
              
                if (Foto != null)
                {
                    Stream str = Foto.InputStream;
                    BinaryReader br = new BinaryReader(str);
                    bytes = br.ReadBytes((Int32)str.Length);
                }
                if (P.Id != 0)
                {
                    var DatoOriginal = context.Personas.Find(P.Id);
                    DatoOriginal.Nombre = P.Nombre;
                    DatoOriginal.FechaNacimiento = Convert.ToDateTime(P.FechaNac);
                    DatoOriginal.Foto = bytes;
                    context.SaveChanges();
                }
                else
                {
                    Personas person = new Personas { Id = P.Id, Nombre = P.Nombre, FechaNacimiento = Convert.ToDateTime(P.FechaNac), Foto = bytes };
                    context.Personas.Add(person);
                    context.SaveChanges();
                    //int id = person.Id;
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
            return View();
        }
        public ActionResult Delete(Int32 IdPersona)
        {
            TestEntities context = new TestEntities();
            try {
                Personas p = context.Personas.First(i => i.Id == IdPersona);
                context.Personas.Remove(p);
                context.SaveChanges();
                return RedirectToAction("Index");
            }catch(Exception)
            {
                //ModelState.AddModelError("", e.Message);
                return RedirectToAction("Error");
            }
        }
        #region  Telefonos

        public ActionResult Telefonos(Int32 IdPersona)
        {
            ViewBag.IdP = IdPersona;
            return View("Telefonos", GetPhones(IdPersona));
        }
        public IEnumerable GetPhones(Int32 IdPersona)
        {
            TestEntities Context = new TestEntities();
            var result = from p in Context.Telefonos
                         where p.IdPersona == IdPersona
                         select p;
            return result.ToList();
        }
        public String Nombre(Int32 IdPersona)
        {
            TestEntities Context = new TestEntities();
            var result = from p in Context.Personas
                         where p.Id == IdPersona
                         select p;
            return result.ElementAt(0).Nombre;
        }
        public ActionResult EditPhone(Int32 IdPhone, String txtPhone, Int32 IdPerson)
        {
            TestEntities Context = new TestEntities();
            if (IdPhone == 0) {
                TestPerson.Telefonos T = new TestPerson.Telefonos { IdPersona = IdPerson, Telefono = txtPhone };
                Context.Telefonos.Add(T);
                Context.SaveChanges();
            }
            else
            {
                var DatoOriginal = Context.Telefonos.Find(IdPhone);
                DatoOriginal.Telefono = txtPhone;
                Context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeletePhone(Int32 Id)
        {
            TestEntities Context = new TestEntities();
            TestPerson.Telefonos T = Context.Telefonos.First(i => i.Id == Id);
            Context.Telefonos.Remove(T);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}