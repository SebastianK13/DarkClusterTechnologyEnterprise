var contactPerson = document.getElementById("contactPerson");
var dropd = document.getElementById("findEmployeeContainer");
var searchResult = null;
var form = document.getElementById("addNewServiceRequest");
var desc = document.getElementById("description");
var topic = document.getElementById("topic");

contactPerson.addEventListener("keyup", function () {
    var type = document.getElementById("contactPerson").value;
    debugger;
    a = dropd.getElementsByTagName("a");
    axios.get("/Application/FindEmployee/", {
        params: {
            phrase: type
        }
    }).then(function (response) {
        debugger;
        searchResult = response.data;
        CreateResultsList(searchResult, a);
    }).catch(function (error) {
        alert("ERROR: " + (error.message | error));
    });
});

function CreateResultsList(searchResult, a) {

    for (i = 0; i < a.length; i++) {
        if (i < searchResult.length) {
            a[i].style.display = "block";
            a[i].innerText = searchResult[i].fullInfo;
            a[i].style.borderBottom = "none";
            debugger;
            if (i == searchResult.length - 1) {
                a[i].style.borderBottom = "1px solid rgba(184,184,184, .8)";
            }
        }
        else {
            a[i].style.display = "none";
        }
    }
}
dropd.addEventListener("click", function (e) {

    if (e.target.tagName == "A") {
        var i = e.target.innerText;
        contactPerson.value = i;

        for (i = 0; i < a.length; i++) {
            a[i].style.display = "none";
        }
    }

});

form.addEventListener("submit", function (e) {
    if (CheckIfEmpty())
        e.preventDefault();
});

function CheckIfEmpty() {

    return desc.value == "" || topic.value == "" || contactPerson.value == "";
}