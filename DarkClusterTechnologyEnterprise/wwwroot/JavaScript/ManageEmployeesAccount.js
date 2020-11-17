var position = document.getElementById("position");
var positionField = document.getElementById("positionField");
var positionContainer = document.getElementById("positionContainer");
var department = document.getElementById("department");
var departmentField = document.getElementById("departmentField");
var departmentContainer = document.getElementById("departmentContainer");
var superior = document.getElementById("superior");
var superiorField = document.getElementById("superiorField");
var superiorContainer = document.getElementById("superiorContainer");

var searchPosIds = [];
var searchDeptIds = [];
var searchSupIds = [];

function CheckIfEmpty() {

    CheckContactPerson();
    CheckTopic();
    CheckDescription();
    AddOrRemoveErrors();

    return desc.value == "" || topic.value == "" || contactPerson.value == "";
}

function AddOrRemoveErrors() {
    if (emptyFields.includes(true) && errorsSection.childElementCount < 1) {
        error = document.createElement('li');
        error.innerText = "Marked fields are mandatory"
        errorsSection.appendChild(error);
    }
    else if (!emptyFields.includes(true)) {
        errorsSection.innerHTML = "";
    }
}

cancell.addEventListener("click", function () {
    desc.value = "";
    topic.value = "";
    contactPerson.value = "";
});

position.addEventListener("keyup", function () {
    var type = position.value;
    debugger;
    a = positionContainer.getElementsByTagName("a");
    axios.get("/Application/FindPosition/", {
        params: {
            phrase: type
        }
    }).then(function (response) {
        debugger;
        var result = response.data;
        ResultsForPosition(result, a);
    }).catch(function (error) {
        alert("ERROR: " + (error.message | error));
    });
});

function ResultsForPosition(result, a) {

    for (i = 0; i < a.length; i++) {
        if (i < result.length) {
            debugger;
            a[i].style.display = "block";
            a[i].innerText = result[i].fullInfo;
            a[i].id = "result" + i;
            searchPosIds[i] = {
                name: a[i].id,
                id: result[i].Id
            };
            a[i].style.borderBottom = "none";
            if (i == result.length - 1) {
                a[i].style.borderBottom = "1px solid rgba(184,184,184, .8)";
            }
        }
        else {
            a[i].style.display = "none";
        }
    }
}
positionContainer.addEventListener("click", function (e) {

    if (e.target.tagName == "A") {
        var i = e.target.innerText;
        position.value = i;
        for (s of searchPosIds) {
            if (s.name == e.target.id) {
                debugger;
                positionField.value = s.id;
            }
        }

        for (i = 0; i < a.length; i++) {
            a[i].style.display = "none";
        }
    }
});

department.addEventListener("keyup", function () {
    var type = department.value;
    debugger;
    a = departmentContainer.getElementsByTagName("a");
    axios.get("/Application/FindDepartment/", {
        params: {
            phrase: type
        }
    }).then(function (response) {
        debugger;
        var result = response.data;
        ResultsForDepartment(result, a);
    }).catch(function (error) {
        alert("ERROR: " + (error.message | error));
    });
});

function ResultsForDepartment(result, a) {

    for (i = 0; i < a.length; i++) {
        if (i < result.length) {
            debugger;
            a[i].style.display = "block";
            a[i].innerText = result[i].fullInfo;
            a[i].id = "result" + i;
            searchDeptIds[i] = {
                name: a[i].id,
                id: result[i].Id
            };
            a[i].style.borderBottom = "none";
            if (i == result.length - 1) {
                a[i].style.borderBottom = "1px solid rgba(184,184,184, .8)";
            }
        }
        else {
            a[i].style.display = "none";
        }
    }
}
departmentContainer.addEventListener("click", function (e) {

    if (e.target.tagName == "A") {
        var i = e.target.innerText;
        department.value = i;
        for (s of searchDeptIds) {
            if (s.name == e.target.id) {
                debugger;
                departmentField.value = s.id;
            }
        }

        for (i = 0; i < a.length; i++) {
            a[i].style.display = "none";
        }
    }
});

superior.addEventListener("keyup", function () {
    var type = superior.value;
    debugger;
    a = superiorContainer.getElementsByTagName("a");
    axios.get("/Application/FindEmployee/", {
        params: {
            phrase: type
        }
    }).then(function (response) {
        debugger;
        var result = response.data;
        ResultsForSuperior(result, a);
    }).catch(function (error) {
        alert("ERROR: " + (error.message | error));
    });
});

function ResultsForSuperior(result, a) {

    for (i = 0; i < a.length; i++) {
        if (i < result.length) {
            debugger;
            a[i].style.display = "block";
            a[i].innerText = result[i].fullInfo;
            a[i].id = "result" + i;
            searchSupIds[i] = {
                name: a[i].id,
                id: result[i].EmployeeId
            };
            a[i].style.borderBottom = "none";
            if (i == result.length - 1) {
                a[i].style.borderBottom = "1px solid rgba(184,184,184, .8)";
            }
        }
        else {
            a[i].style.display = "none";
        }
    }
}
superiorContainer.addEventListener("click", function (e) {

    if (e.target.tagName == "A") {
        var i = e.target.innerText;
        superior.value = i;
        for (s of searchSupIds) {
            if (s.name == e.target.id) {
                debugger;
                superiorField.value = s.id;
            }
        }

        for (i = 0; i < a.length; i++) {
            a[i].style.display = "none";
        }
    }
});