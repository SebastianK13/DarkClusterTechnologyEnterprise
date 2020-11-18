var position = document.getElementById("position");
var positionField = document.getElementById("positionField");
var positionContainer = document.getElementById("positionContainer");
var department = document.getElementById("department");
var departmentField = document.getElementById("departmentField");
var departmentContainer = document.getElementById("departmentContainer");
var superior = document.getElementById("superior");
var superiorField = document.getElementById("superiorField");
var superiorContainer = document.getElementById("superiorContainer");
var email = document.getElementById("emailField");
var date = document.getElementById("startDate");

var firstname = document.getElementById("firstnameField");
var surname = document.getElementById("surnameField");
var country = document.getElementById("countryField");
var city = document.getElementById("cityField");
var address = document.getElementById("addressField");
var zipCode = document.getElementById("zipCodeField");

var formFieldsArr = [firstname, surname, country, city,
    address, zipCode, desc, topic, contactPerson,
    position, department, superior, date, email];

var emptyFields = [false, false, false,
    false, false, false, false, false,
    false, false, false, false, false, false];

var searchPosIds = [];
var searchDeptIds = [];
var searchSupIds = [];

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
                id: result[i].id
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
                id: result[i].id
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
                id: result[i].employeeId
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

function CheckIfEmpty() {

    CheckOtherFields();
    AddOrRemoveErrors();

    return AddOrRemoveErrors();
}

function AddOrRemoveErrors() {
    var incorrect = true;
    if (emptyFields.includes(true) && errorsSection.childElementCount < 1) {
        error = document.createElement('li');
        error.innerText = "Marked fields are mandatory"
        errorsSection.appendChild(error);
    }
    else if (!emptyFields.includes(true)) {
        errorsSection.innerHTML = "";
        incorrect = false;
    }
    return incorrect;
}

function CheckOtherFields() {
    for (i = 0; i < formFieldsArr.length; i++) {
        if (formFieldsArr[i].value == "") {
            formFieldsArr[i].style.border = "1px solid rgba(255,0,0, .8)";
            emptyFields[i] = true;
        }
        else {
            formFieldsArr[i].style.border = "";
            emptyFields[i] = false;
        }
    }
}

contactPerson.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

position.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

firstname.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

surname.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

country.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

city.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

address.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

department.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

superior.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

zipCode.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

date.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

email.addEventListener("change", function () {
    CheckOtherFields();
    AddOrRemoveErrors();
});

function SetMinDate() {
    var today = new Date();

    var d = modifyDay(today);
    var m = modifyMonth(today);
    var y = today.getFullYear();

    var maxVal = new Date(y + '-' + m + '-' + d);
    maxVal.setDate(maxVal.getDate() + 30);

    var dM = modifyDay(maxVal);
    var mM = modifyMonth(maxVal);
    var yM = maxVal.getFullYear();

    startDate.min = y + '-' + m + '-' + d;
    startDate.value = y + '-' + m + '-' + d;
    startDate.max = yM + '-' + mM + '-' + dM;
}
function modifyMonth(today) {
    var m = today.getMonth() + 1;
    if (m < 10) {
        m = "0" + m;
    }
    return m;
}
function modifyDay(today) {
    var d = today.getDate();
    if (d < 10) {
        d = "0" + d;
    }
    return d;
}
cancell.addEventListener("click", function () {
    for (i = 0; i < formFieldsArr.length; i++) {
        formFieldsArr[i].value = "";
        formFieldsArr[i].style.border = "";
        emptyFields[i] = false;
    }
    AddOrRemoveErrors();
});