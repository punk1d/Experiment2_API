using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using Experiment2_API.Models;
using Experiment2_API.DB;
using MySql.Data.MySqlClient;

namespace Experiment2_API.Controllers
{
    [CollectionDataContract]
    public class ClientsController : ApiController
    {
        protected HttpResponseMessage ApiResponse = null;
        protected MySqlDataReader result;
        protected int InsertResult;

        public IHttpActionResult Post(ClientModels NewClient)
        {
            //incluir reglas de negocio aqui, validar la info que vamos a postear
            var Parameters = new List<DbParameter>();

            Parameters.Add(new DbParameter() { Name = "Username", Value = NewClient.Username });
            Parameters.Add(new DbParameter() { Name = "Password", Value = NewClient.Password });
            Parameters.Add(new DbParameter() { Name = "name", Value = NewClient.name });
            Parameters.Add(new DbParameter() { Name = "Lastname", Value = NewClient.Lastname });
            Parameters.Add(new DbParameter() { Name = "phone", Value = NewClient.phone });
            Parameters.Add(new DbParameter() { Name = "email", Value = NewClient.email });

            var ScriptGenerator = new SQLScriptGenerator();
            var DbConnector = new DbConnector();

            var script = ScriptGenerator.GenerateInsertScript("client", Parameters);
            try
            {
                InsertResult = DbConnector.ExecuteNonQueryScript(script);
            }
            catch (Exception ex)
            {
                ApiResponse = Request.CreateResponse(HttpStatusCode.OK,
                     "There was an error in the process. \n Message: " + ex.Message);
                return ResponseMessage(ApiResponse);
            }
            ApiResponse = Request.CreateResponse(HttpStatusCode.OK, InsertResult);
            return ResponseMessage(ApiResponse);
        }


    }
}