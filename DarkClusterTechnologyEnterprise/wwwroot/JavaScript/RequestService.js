var emptyFields = [false, false, false];

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