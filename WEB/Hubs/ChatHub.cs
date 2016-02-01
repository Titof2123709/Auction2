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
//using System.Timers;
using System.Web;
using WEB.Models;

namespace WEB.Hubs
{
    //почему-то autorize не работает! 
    //[Authorize(Roles = "user,admin")]
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

        //добавление пользователя в группу
        public void JoinRoom(string roomName,string userName)
        {
            var id = Context.ConnectionId;
            //если не содержит id
            if (!Users.Any(x => x.ConnectionId == id))
            {
                //добавляем пользователя
                Users.Add(new ChatModel { ConnectionId = id, Name = userName });

                // Посылаем сообщение текущему пользователю
           //     Clients.Caller.onConnected(id, userName, Users);

                // Посылаем сообщение всем пользователям, кроме текущего
            //    Clients.AllExcept(id).onNewUserConnected(id, userName);
            }

            //добавляем пользователя в группу
             Groups.Add(Context.ConnectionId, roomName);

             //вызываем установку личн данных пользователя в скрытых поля и приметствие!
             Clients.Caller.onConnected(id,userName);

            var timermodel = TimerTasks.FirstOrDefault(item => item.GroupName.Equals(roomName));
                //проверяем создана ли группа с таймером
             if (timermodel == null)
             {
                 //не уверен в надобности этого!!!!!!!!!!!
                 lock (obj)
                 {
                 //не связан с Timer в коллекции 
                     //проверить что дает этот Time = default(DateTime)!!!!!!!!!!!!!!!
                     var newtimermodel = new TimerTaskModel { GroupName = roomName, Timer = Task.FromResult(Timer(roomName)), Message = string.Empty };
                 //добавляем объект в List
                 TimerTasks.Add(newtimermodel);
                }
             }
            
           // Вызов метода только на текущем клиенте, который обратился к серверу
            //хз как тут с временем!!!!!!!!!!!!!!!
             Clients.Caller.addMessage(default(DateTime), userName, TimerTasks.First(model => model.GroupName.Equals(roomName)).Message);

        }


        //послать сообщение определенной группе
        public void SendToGroup(string roomName, string username, string message)
        {
            //меняем сообщение и перезапускаем таймер
            TimerTasks.Where(model => model.GroupName.Equals(roomName)).Select(
                timer => 
                {
                    timer.Timer.Dispose();
                    //return Task.FromResult(Timer(roomName)) - Проверить точно ли записалось новое задание в коллекцию!!!!
                    timer.Timer = Task.FromResult(Timer(roomName));
                    timer.Message = message;
                    //не уверен в правильности!!!!проверить!!!!!!!!!!!!!
                    return timer;
                
                });
            //добавляем сообщение в группу!
            Clients.Group(roomName).addChatMessage(DateTime.Now, username, message);
        }


        // Отключение пользователя
        //Отключение от группы происходит автоматом - но я написал сам на всякий!!
        //You should not manually remove the user from the
       // group when the user disconnects. This action is automatically 
        //    performed by the SignalR framework.
        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
              //  var id = Context.ConnectionId;
             //   Clients.All.onUserDisconnected(id, item.Name);
            }
            Groups.Remove(Context.ConnectionId, item.GroupName);

            return base.OnDisconnected(stopCalled);
        }



        private async Task Timer(string room)
        {
            for (int i = 20; i >= 0; i--)
            {               
                await Task.Delay(1000);
                //отправляем секунды во view 
                Clients.Group(room).addTimerTicks(i);
            }
            //проверить на правильность!!!!!!!!!!!!!
            var Winner = TimerTasks.First(model => model.GroupName.Equals(room));
            TimerTasks.RemoveAll(model => model.GroupName.Equals(room));


            //если никто не отписался..что-то сделать...!
            //получаем имя победителя
            string winnername = Users.Find(winner=>winner.GroupName==room).Name;
            Clients.Group(room).disconnectPage(winnername,room,DateTime.Now);
            //проверить работает ли!!!!!!!!!!!!!!
            //антипаттерн сервис локатор..посмотреть решение проблемы в DI для хаба наверн!!
            //изменить statys этого лота!!!
            var lot = GetCabinetService.GetLotByName(Winner.GroupName);
            lot.StatysId = (int)Statys.Sold;
            lot.EndPrice = Convert.ToInt32(Winner.Message);
            lot.BuyerName = winnername;
            GetCabinetService.UpdateLot(lot);
           // ListAllLotNames();
            //return View("ShowLot", lotmodel);
        
           

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