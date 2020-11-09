var newTaskBtn = document.getElementById("addNewTask");
var cover = document.getElementById("cover");
var cancell = document.getElementById("newTaskCancell");
var save = document.getElementById("newTaskSave");
var tasksList = null;
var datesList = null;
var datesNTime = null;
var dates = [];
var actualPage = 0;
var datesHeader = document.getElementById("headerDates").getElementsByTagName("div");
var rightArrow = document.getElementById("rightArrowT");
var leftArrow = document.getElementById("leftArrowT");
var index = 0;
var days = document.getElementById("days").children;
var callendarArea = document.getElementById("days");
var backgrounds = ["background-first", "background-second", "background-third", "background-fourth"];
var form = document.getElementById("addNewTaskForm");
//NewTask post validation
var emptyFields = [true, true, true, true, true, true];
var task = document.getElementById("Task");
var taskDesc = document.getElementById("TaskDesc");
var startTime = document.getElementById("startTime");
var startDate = document.getElementById("startDate");
var endTime = document.getElementById("endTime");
var endDate = document.getElementById("endDate");

function CreateDatesNTime() {
    var fullDatesList = [];
    for (i = 0; i < tasksList.length; i++) {
        fullDatesList[i] = {
            beginDate: new Date(tasksList[i].taskBegin),
            endDate: new Date(tasksList[i].taskDeadline)
        };
    }
    datesNTime = fullDatesList;
}

task.addEventListener("keyup", function () {
    CheckIsEmptyField(null);
});

taskDesc.addEventListener("keyup", function () {
    CheckIsEmptyField(null);
});

startTime.addEventListener("change", function () {
    CheckIsEmptyField(null);
});

startDate.addEventListener("change", function () {
    CheckIsEmptyField(null);
});

endTime.addEventListener("change", function () {
    CheckIsEmptyField(null);
});

endDate.addEventListener("change", function () {
    CheckIsEmptyField(null);
});

callendarArea.addEventListener("mouseenter", function (e) {
    if (e.target.classList.contains("new-task")) {
        var height = e.target.offsetHeight;
        var autoH = e.target.scrollHeight;

        if (height < autoH)
            e.target.classList.add("autoHeight");
    }
}, true);

callendarArea.addEventListener("mouseleave", function (e) {
    if (e.target.classList.contains("new-task")) {
        e.target.classList.remove("autoHeight");
    }
}, true);

newTaskBtn.addEventListener("click", function () {
    cover.classList.remove("display-none");
    SetNewTaskDate();
    SetNewTaskTime();
});

cancell.addEventListener("click", function () {
    cover.classList.add("display-none");
});

leftArrow.addEventListener("click", function () {
    if (actualPage > 0) {
        actualPage -= 1;
        if (actualPage === 0) {
            leftArrow.classList.add("display-none");
        }
        else if (actualPage === 3) {
            rightArrow.classList.remove("display-none");
        }
        SetIndex();
        ChangeHeaderDates();
        RemoveAllTasks();
        GenerateTasks();
    }
});

rightArrow.addEventListener("click", function () {
    if (actualPage < 4) {
        actualPage += 1;
        if (actualPage === 4) {
            rightArrow.classList.add("display-none");
        }
        else if (actualPage === 1) {
            leftArrow.classList.remove("display-none");
        }
        SetIndex();
        ChangeHeaderDates();
        RemoveAllTasks();
        GenerateTasks();
    }
});

function RemoveAllTasks() {
    callendarArea.innerHTML = '';

    var col1 = document.createElement('div');
    col1.className = "mon-col";
    callendarArea.appendChild(col1);

    var col2 = document.createElement('div');
    col2.className = "tue-col";
    callendarArea.appendChild(col2);

    var col3 = document.createElement('div');
    col3.className = "wed-col";
    callendarArea.appendChild(col3);

    var col4 = document.createElement('div');
    col4.className = "thu-col";
    callendarArea.appendChild(col4);

    var col5 = document.createElement('div');
    col5.className = "fri-col";
    callendarArea.appendChild(col5);

    var col6 = document.createElement('div');
    col6.className = "sat-col";
    callendarArea.appendChild(col6);

    var col7 = document.createElement('div');
    col7.className = "sun-col";
    callendarArea.appendChild(col7);
}

function SetIndex() {
    switch (actualPage) {
        case 0:
            index = 0;
            break;
        case 1:
            index = 7;
            break;
        case 2:
            index = 14;
            break;
        case 3:
            index = 21;
            break;
        case 4:
            index = 23;
            break;
    }
}

function SetTaskModel(tasks, dates) {
    tasksList = tasks;
    datesList = dates;
};
function SetDates() {
    var i = 0;
    for (var d of datesList) {
        var date = d;
        var arr = JSON.stringify(date).split('T');
        dates[i] = arr[0].toString().replace('"', '');
        i++;
    }
}
function ChangeHeaderDates() {
    const months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

    var dateRange = document.getElementById("dateRangeSection");
    var fSD = new Date(dates[index]);
    var fED = new Date(dates[index + 6]);
    var headerDateRange = modifyDay(fSD) + ' ' + months[fSD.getMonth()] + ' ' + fSD.getFullYear()
        + ' ' + '-' + ' ' + modifyDay(fED) + ' ' + months[fED.getMonth()] + ' ' + fED.getFullYear();
    dateRange.innerText = headerDateRange;
    for (i = 0; i < 7; i++) {
        datesHeader[i].innerText = dates[index + i];
    }
}
function GenerateTasks() {
    var lastId = null;
    var backgroundIndex = 0;
    for (p of tasksList) {
        var i = 0;
        for (d of datesHeader) {
            var begin = new Date(p.taskBegin);
            var end = new Date(p.taskDeadline);
            var y = ((Math.abs(end - begin)) / 3600000) * 44;
            var top = ((begin.getHours() * 60 + begin.getMinutes()) / 60) * 44;

            var temp = p.taskBegin;
            var arr = JSON.stringify(temp).split('T');
            var formatedDate = arr[0].toString().replace('"', '');
            var beginTime = arr[1].toString().replace('"', '');


            var temp2 = p.taskDeadline;
            var arr2 = JSON.stringify(temp2).split('T');
            var endTime = arr2[1].toString().replace('"', '');

            if (formatedDate === d.innerText) {
                var newTask = document.createElement("div");
                var title = document.createElement("div");
                title.className = "title";
                title.innerText = p.task;
                var time = document.createElement("div");
                time.className = "time"
                time.innerText = beginTime + " - " + endTime;
                var desc = document.createElement("div");
                desc.className = "desc";
                desc.innerText = p.taskDesc;

                newTask.appendChild(time);
                newTask.appendChild(title);
                newTask.appendChild(desc);
                newTask.classList.add("new-task");

                if (lastId === p.taskId) {
                    newTask.classList.add(backgrounds[lastIdBckg]);
                    backgroundIndex--;
                }
                else {
                    newTask.classList.add(backgrounds[backgroundIndex]);
                }


                newTask.style.top = top + "px";
                newTask.style.height = y + "px";
                days[i].appendChild(newTask);
                var lastIdBckg = backgroundIndex;
                backgroundIndex++;;
                if (backgroundIndex > 3)
                    backgroundIndex = 0;

                lastId = p.taskId;                
            }
            i++;
        }
    }
}

form.addEventListener("submit", function (e) {
    CheckIsEmptyField(e);
    if (CheckTaskLength() <= 2) {
        CheckDate();
        var taskLength = document.getElementById("taskLength");
        if (taskLength !== null) {
            taskLength.remove();
        }
    }
    else {
        var error = [];
        error[0] = {
            err: "Maximum task length is 48 hours",
            id: "taskLength"
        };
        GenerateEmptyFieldErr(error);
    }

    var errorsCount = document.getElementById("dateErrors").childElementCount;
    debugger;
    if (errorsCount > 0)
        e.preventDefault();

});

function CheckTaskLength() {

    return ((new Date(endDate.value) - new Date(startDate.value))/3600000)/24;
}

function CheckIsEmptyField(e) {
    array = [];
    var count = 0;

    var arr = [taskDesc, task, startTime, startDate, endTime, endDate];

    var desc = document.getElementById("Desc");
    var name = document.getElementById("Name");
    var stime = document.getElementById("sTime");
    var sdate = document.getElementById("sDate");
    var etime = document.getElementById("eTime");
    var edate = document.getElementById("eDate");

    for (f of arr) {
        switch (f.id) {
            case "TaskDesc":
                if (desc === null && f.value === "") {
                    array[count] = {
                        err: "Description field cannot be empty",
                        id: "Desc"
                    };
                    emptyFields[0] = true;
                    count++;
                }
                else if (f.value != "") {
                    if (desc != null)
                        desc.remove();

                    emptyFields[0] = false;
                }
                break;
            case "Task":
                if (name === null && f.value === "") {
                    array[count] = {
                        err: "Name field cannot be empty",
                        id: "Name"
                    };
                    emptyFields[1] = true;
                    count++;
                }
                else if (f.value != "") {
                    if (name != null)
                        name.remove();

                    emptyFields[1] = false;
                }

                break;
            case "startTime":
                if (stime === null && f.value === "") {
                    array[count] = {
                        err: "Start time field cannot be empty",
                        id: "sTime"
                    };
                    emptyFields[2] = true;
                    count++;
                }
                else if (f.value != "") {
                    if (stime != null)
                        stime.remove();

                    emptyFields[2] = false;
                }

                break;
            case "startDate":
                if (sdate === null && f.value === "") {
                    array[count] = {
                        err: "Start date field cannot be empty",
                        id: "sDate"
                    };
                    emptyFields[3] = true;
                    count++;
                }
                else if (f.value != "") {
                    if (sdate != null)
                        sdate.remove();

                    emptyFields[3] = false;
                }

                break;
            case "endTime":
                if (etime === null && f.value === "") {
                    array[count] = {
                        err: "End time field cannot be empty",
                        id: "eTime"
                    };
                    emptyFields[4] = true;
                    count++;
                }
                else if (f.value != "") {
                    if (etime != null)
                        etime.remove();

                    emptyFields[4] = false;
                }

                break;
            case "endDate":
                if (edate === null && f.value === "") {
                    array[count] = {
                        err: "End date field cannot be empty",
                        id: "eDate"
                    };
                    emptyFields[5] = true;
                    count++;
                }
                else if (f.value != "") {
                    if (edate != null)
                        edate.remove();

                    emptyFields[5] = false;
                }

                break;
        }
    }

    if (e != null && emptyFields.includes(true)) {
        e.preventDefault();
    }
    //else {
    //    SetUTCDatesNTimes();
    //}

    if (array.length > 0) {
        GenerateEmptyFieldErr(array);
    }
}
function GenerateEmptyFieldErr(array) {
    var errSection = document.getElementById("dateErrors");
    for (e of array) {
        var li = document.createElement("li");
        li.innerText = e.err;
        li.id = e.id;
        errSection.appendChild(li);
    }
}

function SetNewTaskDate() {
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

    endDate.min = y + '-' + m + '-' + d;
    endDate.value = y + '-' + m + '-' + d;
    endDate.max = yM + '-' + mM + '-' + dM;

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

function SetNewTaskTime() {
    var today = new Date();
    today.setMinutes(today.getMinutes() + 5);
    var todayEnd = new Date();
    todayEnd.setMinutes(todayEnd.getMinutes() + 10);

    endTime.value = ModifyTime(todayEnd.getHours()) + ':' + ModifyTime(todayEnd.getMinutes()) + ':' + "00";
    startTime.value = ModifyTime(today.getHours()) + ':' + ModifyTime(today.getMinutes()) + ':' + "00";
}
function ModifyTime(today) {
    if (today < 10) {
        today = "0" + today;
    }
    return today;
}
function CheckDate() {
    var newTaskStart = new Date(startDate.value + ' ' + startTime.value);
    var newTaskEnd = new Date(endDate.value + ' ' + endTime.value);
    var errors = [];
    var i = 0;
    var newTaskDateError = document.getElementById("newTaskDateErr");
    if (newTaskDateError != null) {
        newTaskDateError.remove();
    }
    var startGreater = document.getElementById("startGreater");

    if (newTaskEnd < newTaskStart) {
        if (document.getElementById("startGreater") !== null)
            document.getElementById("startGreater").remove();

        errors[0] = {
            err: "End time must be greater than start time or equal",
            id: "startGreater"
        };
    }
    else {

        if (startGreater !== null) {
            startGreater.remove();
        }

        var oneDay = IsDayEqual(newTaskStart, newTaskEnd);

        for (c = 0; c < datesNTime.length; c++) {
            if (oneDay) {
                debugger;
                if (IsDayEqual(newTaskStart, datesNTime[c].beginDate)) {
                    if ((newTaskStart > datesNTime[c].beginDate) && (newTaskEnd < datesNTime[c].endDate)) {
                        errors[i] = {
                            err: "There is a task in this time period",
                            id: "newTaskDateErr"
                        };
                        c = datesNTime.length;
                    }
                    else if ((newTaskStart > datesNTime[c].beginDate) && (newTaskEnd > datesNTime[c].endDate) && (newTaskStart < datesNTime[c].endDate)) {

                        errors[i] = {
                            err: "New task start time must be greater",
                            id: "newTaskDateErr"
                        };
                        i++;
                        c = datesNTime.length;
                    }
                    else if ((newTaskStart < datesNTime[c].beginDate) && (newTaskEnd < datesNTime[c].endDate) && (datesNTime[c].beginDate < newTaskEnd)) {

                        errors[i] = {
                            err: "New task end time must be lesser",
                            id: "newTaskDateErr"
                        };
                        i++;
                        c = datesNTime.length;
                    }
                }
            }
            else {
                debugger;
                if ((newTaskStart < datesNTime[c].beginDate) && (newTaskEnd > datesNTime[c].beginDate)) {
                    errors[i] = {
                        err: "There is a task in this time period",
                        id: "newTaskDateErr"
                    };
                    c = datesNTime.length;
                }
                else if ((newTaskStart > datesNTime[c].beginDate) && (newTaskStart < datesNTime[c].endDate)) {
                    errors[i] = {
                        err: "There is a task in this time period",
                        id: "newTaskDateErr"
                    };
                    c = datesNTime.length;
                }
            }
        }
    }
    debugger;
    GenerateEmptyFieldErr(errors)
}
function IsDayEqual(start, end) {
    var t1 = start.getFullYear() + '-' + start.getMonth() + '-' + start.getDate();
    var t2 = end.getFullYear() + '-' + end.getMonth() + '-' + end.getDate();
    //start = JSON.stringify(start).split('T')[0].toString().replace('"', '');
    //end = JSON.stringify(end).split('T')[0].toString().replace('"', '');
    if (t1.localeCompare(t2) === 0) {

        return true;
    }
    else {
        return false;
    }
}