using ITSingular.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSingular.WebAPI.DAL
{
    public interface ICommunicationRepository
    {
        Communication SetMachineState(string machineName);
        int GetCountMachineOnline();
        int GetCountMachineOffline();
        int GetCountMachineAlert();
    }
}