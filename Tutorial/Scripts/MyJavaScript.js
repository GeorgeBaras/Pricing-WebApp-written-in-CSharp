
var acc = document.getElementsByClassName("accordion");
var i;

for (i = 0; i < acc.length; i++) {
    acc[i].onclick = function () {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        if (panel.style.display === "block") {
            panel.style.display = "none";
        } else {
            panel.style.display = "block";
        }
    }
}

function validateSaveEditedPRForm() {
    var x = document.getElementById("LookupCode").innerHTML;
    // alert("LookupCodes are not supposed to contain spaces..."+x);
    if (x.contains(" ")) {
        alert("LookupCodes are not supposed to contain spaces, please retry...");
        return false;
    }
}