$(function () {

    $('#chatBody').show();
  //  $('#loginBlock').show();
    // Ссылка на автоматически-сгенерированный прокси хаба
    //вот эта строчка заставляет заходить в класс и инициализ static List!!!
    var chat = $.connection.chatHub;




    // Объявление функции, которая хаб вызывает при получении сообщений
    chat.client.addMessage = function (time, name, message) {
        // Добавление сообщений на веб-страницу 
        $('#chatroom').append('<p><b>' + htmlEncode(name)
            + '</b>: ' + htmlEncode(message) + '  </b>: ' + htmlEncode(time) + '</p>');
    };



    // Функция, вызываемая при подключении нового пользователя
    chat.client.onConnected = function (id, userName) {

        $('#chatBody').show();
        // установка в скрытых полях имени и id текущего пользователя
        $('#hdId').val(id);
        $('#username').val(userName);
        $('#header').html('<h3>Добро пожаловать, ' + userName + '</h3>');

        // Добавление всех пользователей
      /*  for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }*/
    }


    // Открываем соединение
    //срабатывает автоматически при загрузке страницы..
    //открывает соединение и все что внутри выполняет!!!
    $.connection.hub.start().done(function () {

        chat.server.joinRoom($('#txtGroupName').val(), $('#txtUserName').val());

        $('#sendmessage').click(function () {
            // Вызываем у хаба метод Send
            chat.server.sendToGroup($('#txtGroupName').val(), $('#txtUserName').val(), $('#message').val());
            $('#message').val('');
        });
       

    });

    //выводит отсчет таймера
    chat.client.addTimerTicks = function(i)
    {
        $('#someidhtml').html('Time: ' + i);
    }

    //выводит отсчет таймера
    chat.client.disconnectPage = function (username,groupname,time)
    {
        $('#chatBody').hide();
        $('#someidhtml').html('<h3>Лот - , '+ groupname +'выйграл: ' + userName +'в '+ time + '</h3>');
    }


});


// Кодирование тегов
function htmlEncode(value) {

    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
