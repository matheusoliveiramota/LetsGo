$(document).ready(function() {
    $('select').material_select();


    $('#calcular').click(function(){
    	var moeda_inicial = $('#moeda_inicial').val();
    	var moeda_final = $('#moeda_final').val();
    	var valor_inicial = $('#vlr_inicial').val();
    	if(moeda_inicial == moeda_final){
    		alert('Você não pode converter para a mesma moeda!');
    		return false;
    	}

    	$.ajax({
                url: 'https://api.vitortec.com/currency/converter/v1.2/?from='+moeda_inicial+'&to='+moeda_final+'&value='+valor_inicial,
                type: "GET",
                dataType: "json",
                beforeSend: function(){
                	$('#spinner').show();
                	$('#calcular').attr('disabled', true);
                	$('#div_inicial').hide();
                	$('#div_final').hide();
                },
                success: function(resposta){
                	$('#spinner').hide();
                	$('#calcular').attr('disabled', false);
                	$('#div_inicial').show();
                	$('#div_final').show();
                	$('#vlr_final').val( resposta.data.toCode + ' ' + resposta.data.resultSimple);
                },
                error: function(){
                	alert('Preencha todos os campos corretamente!');
                	$('#calcular').attr('disabled', false);
                	$('#spinner').hide();
                	$('#div_inicial').show();
                	$('#div_final').show();
                }
        }); /* fim da função ajax */
        return false;
    });
});