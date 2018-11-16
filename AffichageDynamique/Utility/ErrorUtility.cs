using AffichageDynamique.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AffichageDynamique.Utility
{
    [Route("error")]
    public class ErrorUtility
    {
        public static List<SQLErrorModel> GetListError(SqlException ex, string filename)
        {
            List<SQLErrorModel> lstErrorMessages = new List<SQLErrorModel>();
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                SQLErrorModel sQLErrorModel = new SQLErrorModel();
                if (ex.Errors[i].Number == 50000)
                {
                    sQLErrorModel.Class = Convert.ToInt32(ex.Errors[i].Class);
                    sQLErrorModel.LineNumber = Convert.ToInt32(ex.Errors[i].LineNumber);
                    sQLErrorModel.Message = Convert.ToString(ex.Errors[i].Message);
                    sQLErrorModel.Number = Convert.ToInt32(ex.Errors[i].Number);
                    sQLErrorModel.Procedure = Convert.ToString(ex.Errors[i].Procedure);
                    sQLErrorModel.State = Convert.ToInt32(ex.Errors[i].State);

                    lstErrorMessages.Add(sQLErrorModel);
                }
                else
                {
                    List<SQLErrorModel> lstErrorMessagesCritique = new List<SQLErrorModel>();

                    sQLErrorModel.Class = Convert.ToInt32(ex.Errors[i].Class);
                    sQLErrorModel.LineNumber = Convert.ToInt32(ex.Errors[i].LineNumber);
                    sQLErrorModel.Message = Convert.ToString("ErreurMessage");
                    sQLErrorModel.Number = Convert.ToInt32(ex.Errors[i].Number);
                    sQLErrorModel.Procedure = Convert.ToString(ex.Errors[i].Procedure);
                    sQLErrorModel.State = Convert.ToInt32(ex.Errors[i].State);

                    lstErrorMessagesCritique.Add(sQLErrorModel);

                    ExceptionUtility.SendSQLErrorToMail(ex, ex.Errors[i], filename);

                    return lstErrorMessagesCritique;
                }
            }

            return lstErrorMessages;
        }

        public static string GetStringError(List<SQLErrorModel> lstErrorMessages)
        {
            return GetStringError(lstErrorMessages, null);
        }

        public static string GetStringError(List<SQLErrorModel> lstErrorMessages, string errorMessage)
        {
            string ErrorMessages = errorMessage;
            if (lstErrorMessages != null)
            {
                for (int i = 0; i < lstErrorMessages.Count; i++)
                {
                    ErrorMessages = ErrorMessages + lstErrorMessages[i].Message + "<br />";
                }
            }
            return ErrorMessages;
        }

        public static bool GetIsError50000(List<SQLErrorModel> lstErrorMessages)
        {
            bool isError50000 = false;

            if (lstErrorMessages != null)
            {
                for (int i = 0; i < lstErrorMessages.Count; i++)
                {
                    if (isError50000 == false && lstErrorMessages[i].Number == 50000)
                    {
                        isError50000 = true;
                    }
                    else if (isError50000 == true && (lstErrorMessages[i].Number == 266 || lstErrorMessages[i].Number == 3903))
                    {
                        isError50000 = true;
                    }
                    else if (isError50000 == false && (lstErrorMessages[i].Number == 266 || lstErrorMessages[i].Number == 3903))
                    {
                        isError50000 = false;
                    }
                    else if (lstErrorMessages[i].Number != 50000 && lstErrorMessages[i].Number != 266 && lstErrorMessages[i].Number != 3903)
                    {
                        isError50000 = false;
                        return isError50000;
                    }
                }
            }
            return isError50000;
        }
    }
}
