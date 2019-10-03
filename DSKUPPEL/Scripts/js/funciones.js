	
/* Para Calendario */
$(document).ready(function()
	{
	$('input.datePicker').datepicker(
		{
		changeMonth: true,
		changeYear: true,
		firstDay: 1,
		});
	});


/* PARA LAS TABLAS DINAMICAS */

$(document).ready(function(){
	/*$('#dataTable').DataTable({
		language: { 'url': 'js/jquery-datatables/languages/es.json' },
		aoColumnDefs: [{ 'bSortable': false, 'aTargets': ['no-sortable'] }]
		});*/
    $('#dataTable').DataTable({
        language: {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ Registros",
            "sZeroRecords": "&nbsp;",
            "sEmptyTable": "&nbsp;",
            "sInfo": "Encontrados: _TOTAL_ Registros (Mostrando del _START_ al _END_)",
            "sInfoEmpty": "* No se han encontrado resultados en la búsqueda",
            "sInfoFiltered": "",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        },
        aoColumnDefs: [{ 'bSortable': false, 'aTargets': ['no-sortable'] }]
    });
});





/* POPUP */
$(document).ready(function()
	{
	$('a.tooltip_a').tooltip(
		{
		position:{
			my: 'center bottom-20',
			at: 'center top',
			using: function(position, feedback){
				$( this ).css(position);
				$('<div>')
				.addClass('arrow')
				.addClass(feedback.vertical)
				.addClass(feedback.horizontal)
				.appendTo(this);
				}
			}
		});
	});

/* FANCYBOX */
$(document).ready(function()
{
    $("#iframe").fancybox({
        'width': '85%',
        'height': '85%',
        'autoScale': false,
        'transitionIn': 'none',
        'transitionOut': 'none',
        'type': 'iframe'
    });
});
