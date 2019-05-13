using System;
using System.Collections.Generic;
using System.Linq;

namespace Experiment2_API.DB
{
    public class SQLScriptGenerator
    {
        string script = string.Empty;

        public string GenerateInsertScript(string Table, List<DbParameter> ScriptParameters)
        {
            script += "INSERT INTO ";
            script += Table;
            script += " (";
            script = AddColumnParameters(ScriptParameters, script);
            script += ") ";
            script = AddParameterValues(ScriptParameters, script);
            script += ");";
            return script;
        }

        public string GenerateSelectScript(string Table, List<DbParameter> ScriptParameters, DbParameter SearchCriteria)
        {
            script += "SELECT ";
            if (ScriptParameters != null)
                script = AddColumnParameters(ScriptParameters, script);
            else
                script += "*";
            script += " FROM ";
            script += Table;
            script += " WHERE ";
            script += SearchCriteria.Name + " = ";
            if (Int32.TryParse(SearchCriteria.Value, out int result))
                script += SearchCriteria.Value + ";";
            else
                script += "'" + SearchCriteria.Value + "'";
            return script;
        }

        private string AddParameterValues(List<DbParameter> Parameters, string script)
        {
            script += "values ('";
            foreach (var i in Parameters)
            {
                script += i.Value;
                if (Parameters.Last() != i)
                    script += "','";
                else
                    script += "'";
            }
            return script;
        }

        protected string AddColumnParameters(List<DbParameter> Parameters, string script)
        {
            foreach (var i in Parameters)
            {
                script += i.Name;
                if (Parameters.Last().Name != i.Name)
                    script += ",";
            }
            return script;
        }
    }
}