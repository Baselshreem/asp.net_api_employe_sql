using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace asp.net_api_employe_sql.Controllers
{
    //[EnableCorsAttribute("*", "*", "*")] 
    //[DisableCors]
    public class employeController : ApiController
    {
        // يجب ان تبدا بكلمةGet او وضع httpget
        //[HttpGet]
        //public IEnumerable<employe> Get()
        //{

        //    using (employedbEntities entities = new employedbEntities())
        //    {
        //        return entities.employes.ToList();
        //    }
        //}




        [basicauth]
        public HttpResponseMessage Get(string gender = "All")
        {
            string username =Thread.CurrentPrincipal.Identity.Name;

            using (employedbEntities entities = new employedbEntities())
            {
                switch (username.ToLower())
                {
                    case "mal":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.employes.Where(e => e.gender.ToLower() == "mal").ToList());
                    case "famel":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.employes.Where(e => e.gender.ToLower() == "famel").ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest);

                }
            }
        }



        //by query string parameters 

        //public HttpResponseMessage Get(string gender = "All")
        //{

        //    using (employedbEntities entities = new employedbEntities())
        //    {
        //        switch (gender.ToLower())
        //        {
        //            case "all":
        //                return Request.CreateResponse(HttpStatusCode.OK, entities.employes.ToList());
        //            case "mal":
        //                return Request.CreateResponse(HttpStatusCode.OK, entities.employes.Where(e => e.gender.ToLower() == "mal").ToList());
        //            case "famel":
        //                return Request.CreateResponse(HttpStatusCode.OK, entities.employes.Where(e => e.gender.ToLower() == "famel").ToList());
        //            default:
        //                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "gender must be have male or famel" + gender + "is not valued ");

        //        }
        //    }
        //}

        public HttpResponseMessage Get(int id)
        {

            using (employedbEntities entities = new employedbEntities())
            {
                var entite = entities.employes.FirstOrDefault(e => e.id == id);


                if (entite != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entite);
                }

                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employe not found id=" + id.ToString());
                }
            }
        }

        public HttpResponseMessage Post([FromBody]employe employe)
        {

            try
            {
                using (employedbEntities entities = new employedbEntities())
                {
                    entities.employes.Add(employe);
                    entities.SaveChanges();
                    var massege = Request.CreateResponse(HttpStatusCode.Created, employe);
                    massege.Headers.Location = new Uri(Request.RequestUri + "/" + employe.id.ToString());
                    return massege;
                }
            }
            catch (Exception ex){

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                    }
        }




        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (employedbEntities entities = new employedbEntities())
                {
                    var enti = entities.employes.FirstOrDefault(e => e.id == id);
                    if (enti != null)
                    {


                        entities.employes.Remove(enti);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, enti);
                    }
                    else
                    {

                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employe not found id=" + id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //FromUri بتمرر قيم من خلال الرابط
        //FromBody لو احطها قبل ال id بتصير اجباري اني امرر قيم في الbode
        public HttpResponseMessage Put(int id,[FromBody] employe employe)
        {

            try
            {
                using (employedbEntities entities = new employedbEntities())
                {



                    var enti = entities.employes.FirstOrDefault(e => e.id == id);
                    if (enti != null)
                    {

                        enti.firstname = employe.firstname;
                        enti.lastname = employe.lastname;
                        enti.gender = employe.gender;
                        enti.salarye = employe.salarye;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, enti);
                    }
                    else
                    {

                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employe not found id=" + id.ToString());
                    }


                     
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
