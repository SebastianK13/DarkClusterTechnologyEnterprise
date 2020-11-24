var date = document.getElementById("startDate");
var services = document.getElementById("services");

var formFieldsArr = [desc, topic, contactPerson, date];
var emptyFields = [false, false, false, false];
var serviceModel = [];

cancell.addEventListener("click", function () {
    desc.value = "";
    topic.value = "";
    contactPerson.value = "";
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

date.addEventListener("change", function () {
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

function SetServicesModel(model) {
    servicesModel = model;

    currentOpt = services.value;
    var serviceName = "Account access closing";
    for (s of servicesModel) {
        if (s.serviceName == serviceName)
            services.value = s.serviceId;
    }
}

services.addEventListener("change", function () {
    currentOpt = services.value;
    var serviceName = "";
    for (s of servicesModel) {
        debugger;
        if (s.serviceId == currentOpt)
            serviceName = s.serviceName;
    }

    debugger;
    if (serviceName == "Account creating for new employee") {
        location.href = "ManageEmployeesAccount";
    }
    else if (serviceName == "Account access closing") {
        location.href = "AccountClosing";
    }
});