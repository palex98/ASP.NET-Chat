function OnLogIn() {
    document.getElementById("sending-message-btn").disabled = false;
    document.getElementById("text-of-message").disabled = false;
    document.getElementById("text-of-message").value = "";
}

function ClearingTextArea() {
    document.getElementById("text-of-message").value = "";
}

function ReplaceEmoji() {
    var block = document.getElementById("message-block");
    block.innerHTML = block.innerHTML.replace(/;1;/g, '<img src="/img/emoji/1.png" width="24">')
        .replace(/;2;/g, '<img src="/img/emoji/2.png" width="24">')
        .replace(/;3;/g, '<img src="/img/emoji/3.png" width="24">')
        .replace(/;4;/g, '<img src="/img/emoji/4.png" width="24">')
        .replace(/;5;/g, '<img src="/img/emoji/5.png" width="24">')
        .replace(/;6;/g, '<img src="/img/emoji/6.png" width="24">')
        .replace(/;7;/g, '<img src="/img/emoji/7.png" width="24">')
        .replace(/;8;/g, '<img src="/img/emoji/8.png" width="24">');
}

window.onload = function () {
    RefreshListOfUsers();
    RefreshListOfMessages();
};

function RefreshListOfUsers() {
    var href1 = "/Chat/GetListOfUsers";
    $("#UpdateListOfUsers").attr("href", href1).click();
    setTimeout("RefreshListOfUsers();", 1000);
}

function RefreshListOfMessages() {
    var href2 = "/Chat/GetListOfMessages";
    $("#UpdateListOfMessages").attr("href", href2).click();
    var scrollinDiv = document.getElementById("scroll");
    scrollinDiv.scrollTop = 9999;

    setTimeout("RefreshListOfMessages();", 700);
}

function AddEmoji(emoji) {
    var area = document.getElementById("text-of-message");
    if (area.disabled != true) {
        area.value += emoji;
        area.focus();
    }
}

var TextArea = document.getElementById("text-of-message");

TextArea.addEventListener("keyup", function (event) {
    event.preventDefault();
    if (event.keyCode === 13) {
        document.getElementById("sending-message-btn").click();
    }
});