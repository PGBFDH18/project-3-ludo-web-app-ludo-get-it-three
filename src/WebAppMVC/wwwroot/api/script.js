var localURL = "https://ludogame.azurewebsites.net/api/ludo/";
var board = document.getElementById('boardDiv');
var rField = document.getElementById('rightField');
var newDiv = document.getElementById('newDiv');
var message = document.getElementById('errorMessage');
var gameStarted = document.getElementById("showCode");
const setGameID = document.getElementById('gameCode');
const rollNumber = document.getElementById('getRollNumber');
const rollNumber2 = document.getElementById('getRollNumber2');
const rollNumber3 = document.getElementById('getRollNumber3');

var currentPlayer = null;
var diceValue = null;
var pieceValue = null;
var oldPosition = null;
var newPosition = null;

function joinGame() {
    str = document.getElementById("gamecode").value
    const p = document.createElement('p');

    if (str.length == 0) {
        p.textContent = "Nothing typed!";
        board.appendChild(p);
        return;
    } else {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {

            data = JSON.stringify(this.responseText);

            if (this.readyState == 4 && this.status == 200) {
                if (board.style.display === "none") {
                    board.style.display = "block";
                    newDiv.style.display = "none";
                }
                p.textContent = this.responseText;
                board.appendChild(p);

            } else {
                if (board.style.display === "none") {
                    board.style.display = "block";
                    newDiv.style.display = "none";
                }
                p.textContent = this.status;
                board.appendChild(p);
            }
        };
        xmlhttp.open("GET", localURL + str + "/getgamedetails", false);
        xmlhttp.send();
    }

}

function rollDice(gameURL) {
    //getPosition(gameURL);
    var result = null;
    $.ajax({
        dataType: "json",
        type: "GET",
        async: false,
        url: localURL + gameURL + "/rolldice",
        success: function (data) {
            alert("success");
            result = data;
        }
    });
    diceValue = result;
    rollNumber.textContent = "You rolled: " + result;
    return rField.appendChild(rollNumber);
}

function startGame(gameURL) {
    var result = null;
    $.ajax({
        type: "PUT",
        async: false,
        url: localURL + gameURL + "/startgame",
        dataType: "json",
        success: function(data){
            result = data;
        }
    });
    return result;
}

function getPosition(gameURL) {
    var result = null;
    var output = [];
    $.ajax({
        type: "GET",
        async: false,
        url: localURL + gameURL + "/players/getallplayers",
        dataType: "json",
        success: function(data){
            result = data;
        }
    });
    
    var stringBuilder = null;
    for(i = 0; i < result.length; i++){
        stringBuilder = (result[i].name) + "'s [ ";
        for(k = 0; k < 4; k++){
            stringBuilder += "PieceID: " + result[i].pieces[k].pieceId  + " has position: " + result[i].pieces[k].position + " ]";
        }
        output.push(stringBuilder);
    }

    rollNumber2.textContent = output;
    
    return rField.appendChild(rollNumber2);
}

function movePiece(gameURL){

    fetchPieceInfo(gameURL, pieceValue);

    var thePiece = "." + currentPlayer + " ." + pieceValue;

    var newP = ".B" + newPosition;
    console.log(newP);
    console.log(thePiece);

    $(thePiece).parent().parent().detach().appendTo(newP);


    $.ajax({
        type: "PUT",
        async: false,
        url: localURL + gameURL + "/movepiece?" + "pieceId=" + pieceValue + "&numberOfFields=" + diceValue,
        dataType: "json",
        success: function(data){
        }
    });
}

function endTurn(gameURL){
    var result = null;
    $.ajax({
        type: "PUT",
        async: false,
        url: localURL + gameURL + "/endturn",
        datatype: "json",
        success: function(data){
            console.log(data);
        }
    });
}

function fetchPieceInfo(gameURL, pieceVal){
    var result = null;
    pieceValue = pieceVal;
    $.ajax({
        type: "GET",
        async: false,
        url: localURL + gameURL + "/players/getallplayers",
        dataType: "json",
        success: function(data){
            result = data;
        }
    });

    found = false;
    for(i = 0; i < result.length; i++){
        for(k = 0; k < 4; k++){
            if(result[i].pieces[k].pieceId == pieceValue && found == false)
            {
                oldPosition = result[i].offset + result[i].pieces[k].position;
                newPosition = oldPosition + diceValue;
                console.log("Old: " + oldPosition);
                console.log("New: " + newPosition);
                console.log("Dice: " + diceValue);
                found = true;
            }
        }
    }
}

function setPieceValue(gameUrl, playId, pieId){
    currentPlayer = playId;
    pieceValue = pieId;
    movePiece(gameUrl);
}