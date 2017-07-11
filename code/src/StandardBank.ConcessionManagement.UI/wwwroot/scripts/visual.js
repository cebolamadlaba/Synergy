function hideElement(name) {
    displayState = "block";
    var icon = document.getElementById("compress-icon");

    if(icon.className === "fa fa-compress")
    {
        displayState = "none";
        icon.className = "fa fa-expand";
    }
    else{
        icon.className = "fa fa-compress";
    }

    var x = document.getElementsByClassName(name);
    for (var i = 0; i < x.length; i++) {
        x[i].style.display = displayState;
    }
}