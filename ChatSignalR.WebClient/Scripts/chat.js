﻿function initUi() {
    let displayName = '';
    while (true) {
        displayName = prompt('Enter your name:', '');
        if (displayName != '') {
            break;
        } else {
            alert('Enter valid name');
        }
    }
    $('#displayname').text(displayName);
    $('#message').focus();
    $('#clearMessages').click(function () {
        $('#discussion').empty();
    });
}

function initChat() {
    $.connection.hub.url = "http://localhost:8090/signalr";
    let chat = $.connection.chatHub;

    chat.client.addMessage = function (name, message) {
        let encodedName = $('<div />').text(name).html();
        let encodedMsg = $('<div />').text(message).html();
        $('#discussion').append('<li><strong>' + encodedName
            + '</strong>:&nbsp;' + encodedMsg + '</li>');
    };

    chat.client.setStatus = function (message) {
        $('#status').text(message);
    };

    $.connection.hub.start().done(function () {
        let sendHandler = function () {
            let message = $('#message').val();
            if (message != '') {
                chat.server.send($('#displayname').text(), message);
                $('#message').val('').focus();
            }
        }

        $('#sendmessage').click(sendHandler);
        $('#message').keydown(function (event) {
            if (event.which == 13) {
                sendHandler();
            }
        });
    });
}

$(function () {
    initUi();
    initChat();
});