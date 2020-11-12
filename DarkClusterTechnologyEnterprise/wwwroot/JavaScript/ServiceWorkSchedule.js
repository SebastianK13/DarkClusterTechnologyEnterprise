
newTaskBtn.addEventListener("click", function () {
    cover.classList.remove("display-none");
    SetNewTaskDate();
    SetNewTaskTime();
});

function CreateDatesNTime() {
    var fullDatesList = [];
    for (i = 0; i < tasksList.length; i++) {
        fullDatesList[i] = {
            beginDate: new Date(tasksList[i].beginDate),
            endDate: new Date(tasksList[i].endDate)
        };
    }
    datesNTime = fullDatesList;
}

function GenerateTasks() {
    var lastId = null;
    var backgroundIndex = 0;
    for (p of tasksList) {
        var i = 0;
        for (d of datesHeader) {
            var begin = new Date(p.beginDate);
            var end = new Date(p.endDate);
            var y = ((Math.abs(end - begin)) / 3600000) * 44;
            var top = ((begin.getHours() * 60 + begin.getMinutes()) / 60) * 44;

            var temp = p.beginDate;
            var arr = JSON.stringify(temp).split('T');
            var formatedDate = arr[0].toString().replace('"', '');
            var beginTime = arr[1].toString().replace('"', '');


            var temp2 = p.endDate;
            var arr2 = JSON.stringify(temp2).split('T');
            var endTime = arr2[1].toString().replace('"', '');

            if (formatedDate === d.innerText) {
                var newTask = document.createElement("div");
                var title = document.createElement("div");
                title.className = "title";
                title.innerText = p.name;
                var time = document.createElement("div");
                time.className = "time"
                time.innerText = beginTime + " - " + endTime;
                var desc = document.createElement("div");
                desc.className = "desc";
                desc.innerText = p.description;
                var responsibleEmployee = document.createElement("div");
                responsibleEmployee.className = "responsible-employee";
                responsibleEmployee.innerText = "Responsible employee: " + p.responsibleEmployee;


                newTask.appendChild(time);
                newTask.appendChild(title);
                newTask.appendChild(responsibleEmployee);
                newTask.appendChild(desc);
                newTask.classList.add("new-task");

                if (lastId === p.id) {
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

                lastId = p.id;
            }
            i++;
        }
    }
}