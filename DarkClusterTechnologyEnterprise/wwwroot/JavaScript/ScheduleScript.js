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
    debugger;
    for (i = 0; i < 7; i++) {
        datesHeader[i].innerText = dates[index +i];
    }
}
function GenerateTasks() {
    for (p of tasksList) {
        var i = 0;
        for (d of datesHeader) {
            var begin = new Date(p.taskBegin);
            var end = new Date(p.taskDeadline);
            var y = ((Math.abs(end - begin)) / 3600000)*44;
            var top = (begin.getHours() * 2640 + begin.getMinutes())/60;

            var temp = p.taskBegin;
            var arr = JSON.stringify(temp).split('T');
            var formatedDate = arr[0].toString().replace('"', '');
            if (formatedDate === d.innerText) {
                debugger;
                var newTask = document.createElement("div");
                newTask.className = "new-task";
                newTask.style.top = top+"px";
                newTask.style.height = y+"px";
                days[i].appendChild(newTask);
            }
            i++;
        }
    }
}
