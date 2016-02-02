using BLL.Interface.Services;
using Microsoft.AspNet.SignalR;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WEB.Models;

namespace WEB.Hubs
{

    public class ChatHub : Hub
    {
        static List<ChatModel> Users;
        static List<TimerTaskModel> TimerTasks; 
        private object obj = new object();
        static ChatHub()
        {
           Users = new List<ChatModel>();
           TimerTasks = new List<TimerTaskModel>();
        }

        [Inject]
        public ICabinetService GetCabinetService { get; set; }


        public void JoinRoom(string roomName,string userName)
        {
            var id = Context.ConnectionId;

            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new ChatModel { ConnectionId = id, Name = userName });
            }

             Groups.Add(Context.ConnectionId, roomName);

             Clients.Caller.onConnected(id,userName);

            var timermodel = TimerTasks.FirstOrDefault(item => item.GroupName.Equals(roomName));
             if (timermodel == null)
             {
                 lock (obj)
                 {
                     var newtimermodel = new TimerTaskModel { GroupName = roomName, Timer = Task.FromResult(Timer(roomName)), Message = string.Empty };
                 TimerTasks.Add(newtimermodel);
                }
             }
            
             Clients.Caller.addMessage(default(DateTime), userName, TimerTasks.First(model => model.GroupName.Equals(roomName)).Message);

        }



        public void SendToGroup(string roomName, string username, string message)
        {

            TimerTasks.Where(model => model.GroupName.Equals(roomName)).Select(
                timer => 
                {
                    timer.Timer.Dispose();

                    timer.Timer = Task.FromResult(Timer(roomName));
                    timer.Message = message;

                    return timer;
                
                });

            Clients.Group(roomName).addChatMessage(DateTime.Now, username, message);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);

            }
            Groups.Remove(Context.ConnectionId, item.GroupName);

            return base.OnDisconnected(stopCalled);
        }



        private async Task Timer(string room)
        {
            for (int i = 20; i >= 0; i--)
            {               
                await Task.Delay(1000);

                Clients.Group(room).addTimerTicks(i);
            }

            var Winner = TimerTasks.First(model => model.GroupName.Equals(room));
            TimerTasks.RemoveAll(model => model.GroupName.Equals(room));



            string winnername = Users.Find(winner=>winner.GroupName==room).Name;
            Clients.Group(room).disconnectPage(winnername,room,DateTime.Now);

            var lot = GetCabinetService.GetLotByName(Winner.GroupName);
            lot.StatysId = (int)Statys.Sold;
            lot.EndPrice = Convert.ToInt32(Winner.Message);
            lot.BuyerName = winnername;
            GetCabinetService.UpdateLot(lot);            

        }

    }
}
/*
    ----All connected clients in a specified group.

    Clients.Group(groupName).addChatMessage(name, message);

    ----All connected clients in a specified group except the specified clients, identified by connection ID.

    Clients.Group(groupName, connectionId1, connectionId2).addChatMessage(name, message);

    ----All connected clients in a specified group except the calling client.

    Clients.OthersInGroup(groupName).addChatMessage(name, message);

*/