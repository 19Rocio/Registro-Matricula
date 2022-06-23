const { data } = require("jquery");

var datatable;

$(document).ready(function () {
   loadDataTable();
});


function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Alumno/ObtenerTodos"
        },
        "columns": [
            { "data": "idalumno", "width": "10%" },
            { "data": "nombres", "width": "10%" },
            { "data": "apellido", "width": "10%" },
            { "data": "nie", "width": "10%" },
            { "data": "fechaNacimiento", "width": "10%" },
            { "data": "telefono", "width": "10%" },
            { "data": "direccion", "width": "10%" },
        ]
    })
}