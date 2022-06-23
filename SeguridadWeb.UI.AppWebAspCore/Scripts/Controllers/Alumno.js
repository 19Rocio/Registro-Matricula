var tabladata;

$(document).ready(function () {

    //validamos el formulario
    $("#formNivel").validate({
        rules: {
            nombres: "required",
            apellidos: "required",
            nie: "required",
            fechanacimiento: "required",
            genero: "required",
            direccion: "required"

        },
        messages: {
            nombres: "Ingresar nombres",
            apellidos: "Ingresar apellidos",
            nie: "Ingresar numero de identificación del estudiante",
            fechanacimiento: "Ingresar fecha nacimiento",
            genero: "Ingresar genero",
            direccion: "Ingresar  direccion"
        }
    });

    $('#txtfechanacimiento').datepicker();

    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url.UrlGetAlumnos,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Nie" },
            { "data": "Nombres" },
            { "data": "Apellidos" },
            { "data": "Dui" },
            {
                "data": "FechaNacimiento", render: function (data) {
                  return ObtenerFormatoFecha(data)
                }
            },
            { "data": "Genero" },
            { "data": "Correo" },
            { "data": "Direccion" },


            {
                "data": "IdAlumno", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>" +
                        "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "90px"
            }

        ],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        }
    });

});

function abrirPopUpForm(json) {

    $("#txtid").val(0);

    if (json != null) {

        $("#txtid").val(json.IdAlumno);
        $("#txtidusuario").val(json.IdUser);
        $("#txtcodigo").val(json.Nie);
        $("#txtdui").val(json.Dui);
        $("#txtnombres").val(json.Nombres);
        $("#txtapellidos").val(json.Apellidos);
        $("#txtfechanacimiento").val(ObtenerFormatoFecha(json.FechaNacimiento));
        $("#cbosexo").val(json.Genero);
        $("#txtpartida").val(json.nPartida);
        $("#txtfolio").val(json.nFolio);
        $("#txtlibro").val(json.nLibro);
        $("#txttomo").val(json.nTomo);
        $("#txtnacionalidad").val(json.Nacionalidad);
        $("#txttelefono").val(json.Telefono);
        $("#txtemail").val(json.Correo);
        $('#txtdireccion').val(json.Direccion);
        $("#txtdepartamento").val(json.Departamento);
        $("#txtcanton").val(json.Canton);
        $("#txtcaserio").val(json.Caserio);
        $("#txtmunicipio").val(json.Municipio);
        $("#txtresidencia").val(json.ZonaResidencia);
        $("#txtcivil").val(json.EstadoCivil);
        $("#txtfamilia").val(json.ConvivenciaFamiliar);



    } else {
        $('#txtidusuario').val();
        $("#txtcodigo").val();
        $("#txtdui").val();
        $("#txtnombres").val();
        $("#txtapellidos").val();
        $("#txtfechanacimiento").val();
        $("#cbosexo").val();
        $("#txtpartida").val();
        $("#txtfolio").val();
        $("#txtlibro").val();
        $("#txttomo").val();
        $("#txtnacionalidad").val();
        $("#txttelefono").val();
        $("#txtemail").val();
        $('#txtdireccion').val();
        $("#txtdepartamento").val();
        $("#txtcanton").val();
        $("#txtcaserio").val();
        $("#txtmunicipio").val();
        $("#txtresidencia").val();
        $("#txtcivil").val();
        $("#txtfamilia").val();
    }

    $('#FormModal').modal('show');

}

function Guardar() {

    if ($("#formNivel").valid()) {

        var request = {
            oAlumno: {
                IdAlumno: $("#txtid").val(),
                IdUser: $("#txtidusuario").val(),
                Nie: $("#txtcodigo").val(),
                Dui: $("#txtdui").val(),
                Nombres: $("#txtnombres").val(),
                Apellidos: $("#txtapellidos").val(),
                FechaNacimiento: $("#txtfechanacimiento").val(),
                Genero: $("#cbosexo").val(),
                nPartida: $("#txtpartida").val(),
                nFolio: $("#txtfolio").val(),
                nLibro: $("#txtlibro").val(),
                nTomo: $("#txttomo").val(),
                Nacionalidad: $("#txtnacionalidad").val(),
                Telefono: $("#txttelefono").val(),
                Correo: $("#txtemail").val(),
                Direccion: $('#txtdireccion').val(),
                Departamento: $("#txtdepartamento").val(),
                Canton: $("#txtcanton").val(),
                Caserio: $("#txtcaserio").val(),
                Municipio: $("#txtmunicipio").val(),
                ZonaResidencia: $("#txtresidencia").val(),
                EstadoCivil: $("#txtcivil").val(),
                ConvivenciaFamiliar: $("#txtfamilia").val()
            }
        }

        jQuery.ajax({
            url: $.MisUrls.url.UrlPostGuardarAlumno,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.resultado) {
                    tabladata.ajax.reload();
                    $('#FormModal').modal('hide');
                } else {

                    swal("Mensaje", "No se pudo guardar los cambios", "warning")
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {

            },
        });

    }

}

function eliminar($idalumno) {
    if (confirm("¿Desea eliminar el alumno seleccionado?")) {
        jQuery.ajax({
            url: $.MisUrls.url.UrlGetEliminarAlumno + "?idalumno=" + $idalumno,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.resultado) {
                    tabladata.ajax.reload();
                } else {
                    swal("Mensaje", "No se pudo eliminar el alumno", "warning")
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {

            },
        });

    }
}

function ObtenerFormatoFecha(datetime) {

    var re = /-?\d+/;
    var m = re.exec(datetime);
    var d = new Date(parseInt(m[0]))


    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = (('' + day).length < 2 ? '0' : '') + day + '-' + (('' + month).length < 2 ? '0' : '') + month + '-' + d.getFullYear();

    return output;
}