var addServiceBtn = document.getElementById("addNewService");
var confirmServiceBtn = document.getElementById("entryNewService");
//var removeServiceBtn = document.getElementById("removeNewService");
var finishInvoice = document.getElementById("invFin");
var dropd = document.getElementById("myDropdown");
var input = document.getElementById("myInput");
var table = document.getElementById("invoiceTable");
var startDate = document.getElementById("startDate");
var endDate = document.getElementById("endDate");
var customerIncor = document.getElementById("customerErr");
var searchResult = null;
var summaryField = null;
var invoiceReady = true;

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("invoiceIcon").classList.add("rotate-static");
    ExpandInvoiceCategory();
});

function filterFunction() {
    var filter = input.value.toUpperCase();
    a = dropd.getElementsByTagName("a");

    axios.get('/Customer/Index/', {
        params: {
            type: filter
        }
    })
        .then(function (response) {
            searchResult = response.data;
            CreateResultsList(searchResult, a);
        })
        .catch(function (error) {
            alert("ERROR: " + (error.message | error));
        });
}
async function postInvoice(postData) {
    debugger;

   await axios({
        method: 'post',
        url: '/Customer/Invoice',
        data: {
            "customerId": postData.customerId,
            "invoiceId": postData.invoiceId,
            "invoiceDate": postData.invoiceDate,
            "invoiceExpires": postData.invoiceExpires,
            "invoiceServices": postData.invoiceServices,
            "summary": postData.summary
        }
    })
       .then(function (response) {
           window.location = '/Customer/Invoice';
        })
        .catch(function (error) {
            console.log(error);
        });


    //axios({
    //    method: 'post',
    //    url: '/Customer/Invoice',
    //    headers: { 'Content-Type': 'application/json' },
    //    data: JSON.stringify(postData)
    //})
    //    .then(function (response) {
    //        console.log(response);
    //    })
    //    .catch(function (error) {
    //        console.log(error);
    //    });

}

startDate.addEventListener("change", function () {
    CheckInvoiceDates();
});
endDate.addEventListener("change", function () {
    CheckInvoiceDates();
});

function CheckStartDate() {
    var errSection = document.getElementById("dateErrors");
    var startDate = document.getElementById("startDate").value;
    var endDate = document.getElementById("endDate").value;
    var startDErr = document.getElementById("invoiceErr");
    var start = null;
    var expire = null;
    var today = new Date();

    var d = modifyDay(today);
    var m = modifyMonth(today);
    var y = today.getFullYear();
    var d1 = modifyD1(today);

    start = y + '-' + m + '-' + d;
    expire = y + '-' + m + '-' + d1;

    if ((startDate < start || (startDate instanceof Date) || startDate === endDate)) {
        if (startDErr === null) {
            var li = document.createElement("li");
            li.textContent = "Invoice date is incorrect";
            li.id = "invoiceErr";
            errSection.appendChild(li);
            invoiceReady = false;
        }
    }
    else if (document.getElementById("invoiceErr")) {
        document.getElementById("invoiceErr").remove();
    }
}
function CheckEndDate() {
    var errSection = document.getElementById("dateErrors");
    var startDate = document.getElementById("startDate").value;
    var endDate = document.getElementById("endDate").value;
    var endDErr = document.getElementById("expiresErr");
    var start = null;
    var expire = null;
    var today = new Date();

    var d = modifyDay(today);
    var m = modifyMonth(today);
    var y = today.getFullYear();
    var d1 = modifyD1(today);

    start = y + '-' + m + '-' + d;
    expire = y + '-' + m + '-' + d1;

    if ((endDate <= expire || (endDate instanceof Date) || endDate < startDate)) {
        if (endDErr === null) {
            var li = document.createElement("li");
            li.textContent = "Invoice expires date is incorrect";
            li.id = "expiresErr";
            errSection.appendChild(li);
            invoiceReady = false;
        }
    }
    else if (document.getElementById("expiresErr")) {
        document.getElementById("expiresErr").remove();
    }
}

function CheckInvoiceDates() {
    var errSection = document.getElementById("dateErrors");
    var startDate = document.getElementById("startDate").value;
    var endDate = document.getElementById("endDate").value;
    var startDErr = document.getElementById("invoiceErr");
    var endDErr = document.getElementById("expiresErr");
    var start = null;
    var expire = null;
    var today = new Date();

    debugger;

    var d = modifyDay(today);
    var m = modifyMonth(today);
    var y = today.getFullYear();
    var d1 = modifyD1(today);

    //if (d < 10)
    //    d = "0" + d;

    //if (m < 10)
    //    m = "0" + m;

    start = y + '-' + m + '-' + d;
    expire = y + '-' + m + '-' + d1;


        if ((startDate < start || (startDate instanceof Date) || startDate === endDate)) {
            if (startDErr === null) {
                var li = document.createElement("li");
                li.textContent = "Invoice date is incorrect";
                li.id = "invoiceErr";
                errSection.appendChild(li);
                invoiceReady = false;
            }
        }
        else if (document.getElementById("invoiceErr")) {
            document.getElementById("invoiceErr").remove();
        }


        if ((endDate < expire || (endDate instanceof Date) || endDate < startDate)) {
            if (endDErr === null) {
                var li = document.createElement("li");
                li.textContent = "Invoice expires date is incorrect";
                li.id = "expiresErr";
                errSection.appendChild(li);
                invoiceReady = false;
            }
        }
        else if (document.getElementById("expiresErr")) {
            document.getElementById("expiresErr").remove();
        }

}

table.addEventListener("click", function (e) {

    if (e.target.className === "new-service-btn" && e.target.id === "") {
        e.target.parentNode.parentNode.remove();
        debugger;
        if (table.childElementCount === 3)
            document.getElementById("summaryAmount").parentNode.remove();
        SummaryAllServices();
    }
    else if (e.target.id === "removeNewService") {
        debugger;
        e.target.parentNode.parentNode.remove();
        addServiceBtn.classList.remove("hide");
        finishInvoice.classList.remove("hide");
        confirmServiceBtn.classList.add("hide");
        SummaryAllServices();
        RemoveunnecessaryErr();
    }
});

function RemoveunnecessaryErr() {
    debugger;
    var errIdsArray = ["serviceErr", "quantityErr", "grossVErr"];
    for (i = 0; i < errIdsArray.length; i++) {
        if (document.getElementById(errIdsArray[i]) !== null)
            document.getElementById(errIdsArray[i]).remove();
    }
}

finishInvoice.onclick = function () {
    invoiceReady = true;
    CheckInvoiceDates();
    InvoiceHasServices();
    var cId = findCustomerId();
    if (cId != -1) {
        if (document.getElementById("incorCustId") != null)
            document.getElementById("incorCustId").remove();
        var invId = document.getElementById("invoiceF").value;
        var invStart = document.getElementById("startDate").value;
        var invExp = document.getElementById("endDate").value;
        var trs = table.getElementsByTagName("tr");
        var tab = [];
        for (i = 1; i < trs.length - 1; i++) {
            var tds = trs[i].getElementsByTagName("td");

            //var tableRow = {
            //    service: tds[0].innerText,
            //    quantity: tds[1].innerText,
            //    grossV: tds[2].innerText
            //};
            //tab[i - 1] = tableRow;

            tab[(i * 3) - 3] = tds[3].innerText;
            tab[(i * 3) - 2] = tds[2].innerText;
            tab[(i * 3) - 1] = tds[1].innerText;
        }
        if (invoiceReady) {
            var postData = {
                customerId: cId,
                invoiceId: invId,
                invoiceDate: invStart,
                invoiceExpires: invExp,
                invoiceServices: tab,
                summary: summaryField.innerText
            };
            postInvoice(postData);
        }

    }
    else {
        CheckCustomerErr();
    }
}

function CheckCustomerErr() {
    var custErr = document.getElementById("incorCustId")

    var cId = findCustomerId();
    if (cId != -1) {
        if (document.getElementById("incorCustId") != null)
            document.getElementById("incorCustId").remove();
    }
    else if (custErr === null) {
        var li = document.createElement("li");
        li.textContent = "Customer name is incorrect or empty";
        li.id = "incorCustId";
        customerIncor.appendChild(li);
    }

}

function CreateResultsList(searchResult, a) {

    for (i = 0; i < a.length; i++) {
        if (i < searchResult.length) {
            a[i].style.display = "block";
            a[i].innerText = searchResult[i].customerName;
        }
        else {
            a[i].style.display = "none";
        }
    }
}
function SetDateInvoice() {
    //var today = new Date().toISOString().slice(0,10);
    var today = new Date();

    var d = modifyDay(today);
    var m = modifyMonth(today);
    var y = today.getFullYear();
    var d1 = modifyD1(today);

    startDate.min = y + '-' + m + '-' + d;
    startDate.value = y + '-' + m + '-' + d;
    endDate.min = y + '-' + m + '-' + d1;
    endDate.value = y + '-' + m + '-' + d1;
}
function modifyMonth(today) {
    var m = today.getMonth() + 1;
    if (m<10) {
        m = "0" + m;
    }
    return m;
}
function modifyDay(today) {
    var d = today.getDate();
    if (d<10) {
        d = "0" + d;
    }
    return d;
}
function modifyD1(today) {
    var d1 = today.getDate() + 1;
    if (d1<10) {
        d1 = "0" + d1;
    }
    return d1;
}
addServiceBtn.onclick = function AddNewService() {
    var newService = document.getElementById("newLine");
    if (newService == null) {
        var tr = document.createElement('tr');
        tr.id = "newLine";
        tr.innerHTML = `   
        <td><btn id="removeNewService" class="new-service-btn">Remove</btn></td>
        <td> <input/></td>
        <td> <input/></td>
            <td><input/></td>`;

        if (table.childElementCount > 1) {
            table.insertBefore(tr, table.lastChild);
        }
        else {
            table.appendChild(tr);
        }

        addServiceBtn.classList.add("hide");
        finishInvoice.classList.add("hide");
        confirmServiceBtn.classList.remove("hide");
        //removeServiceBtn.classList.remove("hide");
    }
}
//removeServiceBtn.onclick = function RemoveNewService() {
//    var newService = document.getElementById("newLine");
//    if (newService != null) {
//        newService.remove();
//        addServiceBtn.classList.remove("hide");
//        finishInvoice.classList.remove("hide");
//        confirmServiceBtn.classList.add("hide");
//        removeServiceBtn.classList.add("hide");
//    }
//}
confirmServiceBtn.onclick = function () {
    var newService = document.getElementById("newLine");
    if (newService != null) {
        var tr = document.createElement('tr');
        var td = document.createElement('td');
        var newRowInputs = newService.getElementsByTagName('input');
        var success = true;
        success = CheckNewRowErr(newRowInputs);

        if (success) {
            for (i = 0; i < 3; i++) {
                if (i === 0) {
                    var removeRowBtn = document.createElement("btn");
                    removeRowBtn.className = "new-service-btn";
                    var td = document.createElement('td');
                    removeRowBtn.innerText = "Remove";
                    td.appendChild(removeRowBtn);
                    td.className = "withoutPadd";
                    tr.appendChild(td);
                }

                var tdText = document.createTextNode(newRowInputs[i].value);
                var td = document.createElement('td');
                td.appendChild(tdText);
                tr.appendChild(td);
            }

            //newService.remove();
            if (table.childElementCount === 1) {
                table.appendChild(tr);
                InvoiceHasServices();
            }

            if (table.childElementCount === 2) {
                table.insertBefore(tr, newService);
                var tr2 = document.createElement('tr');
                var td1 = document.createElement('td');
                td1.className = "tdInvisible";
                var td2 = document.createElement('td');
                td2.className = "tdInvisible";
                var td3 = document.createElement('td');
                td3.className = "tdInvisible";
                var td4 = document.createElement('td');
                td4.id = "summaryAmount";
                tr2.appendChild(td1);
                tr2.appendChild(td2);
                tr2.appendChild(td3);
                tr2.appendChild(td4);
                table.appendChild(tr2);
            }
            else {
                table.insertBefore(tr, newService);
            }

            //addServiceBtn.classList.remove("hide");
            //finishInvoice.classList.remove("hide");
            //confirmServiceBtn.classList.add("hide");
            //removeServiceBtn.classList.add("hide");

            summaryField = document.getElementById("summaryAmount");

            ClearNeRowInputs(newRowInputs);

            if (summaryField != null)
                SummaryAllServices(table);

            InvoiceHasServices();
        }
    }
}

function ClearNeRowInputs(newRowInputs) {
    for (i = 0; i < 3; i++) {
        newRowInputs[i].value = "";
    }
}

function CheckNewRowErr(newRowInputs) {
    var success = true;
    var errorsUl = document.getElementById("rowError");
    var fieldsNameArray = ["Service", "Quantity", "Gross value"];
    var errIdsArray = ["serviceErr", "quantityErr", "grossVErr"];
    for (i = 0; i < newRowInputs.length; i++) {
        if (isEmptyOrWhiteSpaces(newRowInputs[i].value)) {
            if (document.getElementById(errIdsArray[i]) != null)
                document.getElementById(errIdsArray[i]).remove();
            var li = document.createElement("li");
            li.id = errIdsArray[i];
            li.textContent = fieldsNameArray[i] + " field cannot be empty";
            errorsUl.appendChild(li);
            success = false;
        }
        else if (document.getElementById(errIdsArray[i]) != null) {
            document.getElementById(errIdsArray[i]).remove();
        }

        if (i > 0 && !isEmptyOrWhiteSpaces(newRowInputs[i].value)) {
            if (isNaN(newRowInputs[i].value)) {
                if (document.getElementById(errIdsArray[i]) != null)
                    document.getElementById(errIdsArray[i]).remove();
                var li = document.createElement("li");
                li.textContent = fieldsNameArray[i] + " accept only numbers";
                li.id = errIdsArray[i];
                errorsUl.appendChild(li);
                success = false;
            }
        }
        else if (document.getElementById(errIdsArray[i]) != null && !isEmptyOrWhiteSpaces(newRowInputs[i].value)) {
            document.getElementById(errIdsArray[i]).remove();
        }
    }

    return success;
}

function isEmptyOrWhiteSpaces(variable) {
    return variable.match(/^ *$/) !== null || variable === null;
}

function SummaryAllServices() {
    trs = table.getElementsByTagName("tr");
    var amount = 0;
    for (i = 1; i < table.childElementCount - 1; i++) {
        var currentTr = trs[i].getElementsByTagName("td");
        amount += Number(currentTr[3].innerText);
    }
    if (summaryField !== null)
        summaryField.innerText = amount;
}

function findCustomerId() {
    var castId = -1;
    if (searchResult != null) {
        for (i = 0; i < searchResult.length; i++) {
            if (searchResult[i].customerName === input.value) {
                castId = searchResult[i].customerId;
            }
        }
    }

    return castId;
}

function InvoiceHasServices() {
    var errorsUl = document.getElementById("rowError");

    if (table.childElementCount < 2) {
        if (errorsUl.childElementCount === 0) {
            var li = document.createElement("li");
            li.id = "servicesNull";
            li.textContent = "Invoice must has at least one service";
            errorsUl.appendChild(li);
            invoiceReady = false;
        }
    }
    else if (document.getElementById("servicesNull") != null) {
        document.getElementById("servicesNull").remove();
    }

}

dropd.addEventListener("click", function (e) {

    if (e.target.tagName == "A") {
        var i = e.target.innerText;
        input.value = i;

        for (i = 0; i < a.length; i++) {
            a[i].style.display = "none";
        }
        completeInvoiceFields(e.target.innerText);
        CheckCustomerErr();
        CheckInvoiceDates();
    }

});

function completeInvoiceFields(name) {
    var email = document.getElementById("emailF");
    var address = document.getElementById("addressF");
    var country = document.getElementById("countryF");
    var city = document.getElementById("cityF");
    var zcode = document.getElementById("zcodeF");
    var email = document.getElementById("emailF");
    var invoiceID = document.getElementById("invoiceF");

    for (i = 0; i < searchResult.length; i++) {
        if (searchResult[i].customerName === name) {
            email.value = searchResult[i].email;
            address.value = searchResult[i].streetAddress;
            country.value = searchResult[i].country;
            city.value = searchResult[i].city;
            zcode.value = searchResult[i].zipCode;
            invoiceID.value = searchResult[i].invoiceId;
            SetDateInvoice();
        }
    }

}