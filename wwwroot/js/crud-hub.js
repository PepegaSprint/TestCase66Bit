
    const connection = new signalR.HubConnectionBuilder()
    .withUrl("/crudHub")
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});

start();



$(function () {
    $('#btnOpenPopup').magnificPopup({
        type: 'inline',
        closeBtnInside: true
    });
});

connection.on("NewPlayerCreated", (obj) => {
    console.log('Created');
    let dateStringArray = obj.birthDate.split('-');
    let dateString = dateStringArray[2].slice(0,-9) + `.` + dateStringArray[1] + `.` + dateStringArray[0];
    let table = document.getElementById("list");
    let row = table.insertRow();
    row.id = obj.id;
    let cell1 = row.insertCell();
    let cell2 = row.insertCell();
    let cell3 = row.insertCell();
    let cell4 = row.insertCell();
    let cell5 = row.insertCell();
    let cell6 = row.insertCell();
    let cell7 = row.insertCell();
    cell1.innerHTML = obj.lastName;
    cell2.innerHTML = obj.firstName;
    cell3.innerHTML = dateString;
    cell4.innerHTML = obj.gender;
    cell5.innerHTML = obj.team;
    cell6.innerHTML = obj.country;
    cell7.innerHTML = 
        `<button type="button" class="btn btn-primary mx-1" onclick="SelectPlayerForEditing(`+ obj.id +` )" >Изменить</button>` +
        `<a class="btn btn-danger mx-1" href="/EditPlayers/Delete?id=` + obj.id + `"> Удалить</a>`;
    if (document.getElementById("tipForEmptyTable") !== null) {
        document.getElementById("tipForEmptyTable").style.visibility = "hidden";
    }
});


connection.on("PlayerDeleted", (obj) => {
    document.getElementById(obj.id).remove();
});


connection.on("PlayerEdited", (obj) => {
    let row = document.getElementById(obj.id);
    let dateStringArray = obj.birthDate.split('-');
    let dateString = dateStringArray[2].slice(0, -9) + `.` + dateStringArray[1] + `.` + dateStringArray[0];
    row.children[0].textContent = obj.lastName;
    row.children[1].textContent = obj.firstName;
    row.children[2].textContent = dateString;
    row.children[3].textContent = obj.gender;
    row.children[4].textContent = obj.team;
    row.children[5].textContent = obj.country;
});




function SelectPlayerForEditing(obj) {
    let docWithDefaultValues = document.getElementById(obj);
    let editingWindow = document.getElementById("form-popup");
    editingWindow.children[8].value = obj;
    editingWindow.children[0].value = docWithDefaultValues.children[0].textContent;
    editingWindow.children[1].value = docWithDefaultValues.children[1].textContent;
    let dateParsedString = docWithDefaultValues.children[2].textContent.split(".")
    editingWindow.children[3].value = dateParsedString[2] + `-` + dateParsedString[1] + `-` + dateParsedString[0];

    if (docWithDefaultValues.children[3].textContent == "жен") {
        editingWindow.children[4].selectedIndex = 1;
    }
    else {
        editingWindow.children[4].selectedIndex = 0;
    }
    editingWindow.children[5].value = docWithDefaultValues.children[4].textContent;



    for (var i = 0; i < editingWindow.children[7].childElementCount; i++) {
        if (docWithDefaultValues.children[5].textContent == editingWindow.children[7].children[i].textContent) {
            editingWindow.children[7].selectedIndex = i;
            break;
        }
    }
    document.getElementById("btnOpenPopup").click()

}


function TransferDataToHref() {
    let editingWindow = document.getElementById("form-popup");

    document.getElementById("hrefForEdit").href = `/EditPlayers/Edit?lastName=` + editingWindow.children[0].value +
        `&firstName=` + editingWindow.children[1].value +
        `&birthDate=` + editingWindow.children[3].value +
        `&gender=` + editingWindow.children[4].value +
        `&team=` + editingWindow.children[5].value +
        `&country=` + editingWindow.children[7].value +
        `&id=` + editingWindow.children[8].value;
    document.getElementById("hrefForEdit").click();
}