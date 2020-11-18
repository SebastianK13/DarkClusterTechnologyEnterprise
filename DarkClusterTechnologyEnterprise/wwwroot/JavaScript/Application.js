//JS file for RequestService.cshtml and ManageEmployeesAccount.cshtml

var contactPerson = document.getElementById("contactPerson");
var dropd = document.getElementById("findEmployeeContainer");
var searchResult = null;
var form = document.getElementById("addNewServiceRequest");
var desc = document.getElementById("description");
var topic = document.getElementById("topic");
var cancell = document.getElementById("newServiceRequestCancell");
var errorsSection = document.getElementById("dateErrors");
var searchIDs = [];

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
            debugger;
            a[i].style.display = "block";
            a[i].innerText = searchResult[i].fullInfo;
            a[i].id = "result" + i;
            searchIDs[i] = {
                name: a[i].id,
                id: searchResult[i].employeeId
            };
            a[i].style.borderBottom = "none";
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
        for (s of searchIDs) {
            if (s.name == e.target.id) {
                debugger;
                document.getElementById("contactPersonField").value = s.id;
            }

        }

        for (i = 0; i < a.length; i++) {
            a[i].style.display = "none";
        }
    }
});

contactPerson.addEventListener("change", function () {
    CheckContactPerson();
    AddOrRemoveErrors();
});
topic.addEventListener("change", function () {
    CheckTopic();
    AddOrRemoveErrors();
});
desc.addEventListener("change", function () {
    CheckDescription();
    AddOrRemoveErrors();
});
form.addEventListener("submit", function (e) {
    if (CheckIfEmpty())
        e.preventDefault();
});

function CheckContactPerson() {
    if (contactPerson.value == "") {
        contactPerson.style.border = "1px solid rgba(255,0,0, .8)";
        emptyFields[0] = true;
    }
    else {
        contactPerson.style.border = "";
        emptyFields[0] = false;
    }
}
function CheckTopic() {
    if (topic.value == "") {
        topic.style.border = "1px solid rgba(255,0,0, .8)";
        emptyFields[1] = true;
    }
    else {
        topic.style.border = "";
        emptyFields[1] = false;
    }
}
function CheckDescription() {
    if (desc.value == "") {
        desc.style.border = "1px solid rgba(255,0,0, .8)";
        emptyFields[2] = true;
    }
    else {
        desc.style.border = "";
        emptyFields[2] = false;
    }
}
