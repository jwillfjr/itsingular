using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using ITSingular.Domain.Entities;

namespace ITSingular.WebAPI.DAL
{
    public class CommunicationRepository : ICommunicationRepository
    {
        string connectionString;

        public CommunicationRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public Communication SetMachineState(string machineName)
        {
            try
            {
                if (string.IsNullOrEmpty(machineName))
                {
                    throw new Exception("Machine name is empty!");
                }
                using (var db = new SqlConnection(connectionString))
                {
                    var communication = db.Query<Communication>("SP_GetCommunicationByMachineName",
                        new { Machine = machineName },
                        commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                    if (communication == null)
                    {
                        communication = new Communication();
                        communication.Machine = machineName;
                    }

                    communication.Last = DateTime.Now;

                    int result = 0;

                    if (communication.Id <= 0)
                    {
                        result = db.Insert(communication);
                        communication.Id = result;
                    }
                    else
                        db.Update(communication);

                    return communication;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCountMachineOffline()
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    return db.Query<int>(@"select count(*) from Communication c	
                                            where DATEDIFF(SECOND,c.Last, GETDATE()) > (60 * 1.5);").FirstOrDefault();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCountMachineOnline()
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    return db.Query<int>(@"select count(*) from Communication c	
                                            where DATEDIFF(SECOND,c.Last, GETDATE()) <= 60;").FirstOrDefault();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCountMachineAlert()
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    return db.Query<int>(@"select count(*) from Communication c	
                                            where DATEDIFF(SECOND,c.Last, GETDATE()) > 60
                                              and DATEDIFF(SECOND,c.Last, GETDATE()) <= (60 * 1.5);").FirstOrDefault();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}