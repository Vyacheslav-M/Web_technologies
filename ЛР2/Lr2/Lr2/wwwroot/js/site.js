function calc() {
    var table = document.getElementById("table");
    var sum = 0;
    let lastRow = table.rows[table.rows.length - 1].cells[4];
    for (var i = 1; i < table.rows.length - 1; i++) {
        let row = table.rows[i];
        sum += Number(row.cells[2].innerText) * Number(row.cells[4].innerText);
    }
    lastRow.innerText = sum;
}

window.onload = function () {
    calc();
}