using Common;
using Common.Models;
using Data;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;


namespace Logic
{
    public class Chat
    {
        IChat Querries = GetClassChat();
        public List<ClientChat> Index(string id, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                return Querries.GetMessages(id, SessionId);
            }
            return null;
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (Querries.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }


        public bool SendMessage(Common.Models.ClientChat chat, string ChatId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.SendMessage(chat, SessionId, ChatId);
                return true;
            }
            return false;
        }

        public bool createReport(int reportReasonform, string comment, string chatId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Report report = new Report() { reportReason = reportReasonform, ReportType = (int)reportTypes.chatHelp, reportComment = comment, reportId = chatId };
                report.creatorId = Querries.GetAccount(SessionId);
                Querries.createReport(SessionId, report);
                return true;
            }
            return false;
        }
    }
}
