var newTaskBtn = document.getElementById("addNewTask");
var cover = document.getElementById("cover");
var cancell = document.getElementById("newTaskCancell");
var save = document.getElementById("newTaskSave");
var tasksList = null;
var datesList = null;
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
    }
});

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
    for (i = 0; i < 7; i++) {
        datesHeader[i].innerText = dates[index + i];
    }
}
function GenerateTasks() {
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
                newTask.classList.add(backgrounds[backgroundIndex]);
                newTask.style.top = top + "px";
                newTask.style.height = y + "px";
                days[i].appendChild(newTask);
                backgroundIndex++;;
                if (backgroundIndex > 3)
                    backgroundIndex = 0;
            }
            i++;
        }
    }
}

form.addEventListener("submit", function (e) {

    CheckIsEmptyField(e);
    //if () {

    //}
});

function CheckIsEmptyField(e) {
    var task = document.getElementById("Task");
    var taskDesc = document.getElementById("TaskDesc");
    var startTime = document.getElementById("startTime");
    var startDate = document.getElementById("startDate");
    var endTime = document.getElementById("endTime");
    var endDate = document.getElementById("endDate");
    array = [];
    var count = 0;

    var arr = [task, taskDesc, startTime, startDate, endTime, endDate];

    for (f of arr) {
        if (f.innerText === "") {
            switch (f.id) {
                case "Task":
                    array[count] = "Task field cannot be empty";
                    break;
                case "TaskDesc":
                    array[count] = "Description field cannot be empty";
                    break;
                case "startTime":
                    array[count] = "Start time field cannot be empty";
                    break;
                case "startDate":
                    array[count] = "Start date field cannot be empty";
                    break;
                case "endTime":
                    array[count] = "End time field cannot be empty";
                    break;
                case "endDate":
                    array[count] = "End date field cannot be empty";
                    break;
            }
            count++;
        }
    }
    debugger;
    if (array.length > 0) {
        e.preventDefault();
        GenerateEmptyFieldErr(array);
    }
}
function GenerateEmptyFieldErr(array) {
    var errSection = document.getElementById("dateErrors");
    for (e of array) {
        var li = document.createElement("li");
        li.innerText = e;
        errSection.appendChild(li);
    }
}