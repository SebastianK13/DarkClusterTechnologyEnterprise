﻿var newTaskBtn = document.getElementById("addNewTask");
var cover = document.getElementById("cover");
var cancell = document.getElementById("newTaskCancell");
var save = document.getElementById("newTaskSave");

newTaskBtn.addEventListener("click", function () {
    cover.classList.remove("display-none");
});

cancell.addEventListener("click", function () {
    cover.classList.add("display-none");
});