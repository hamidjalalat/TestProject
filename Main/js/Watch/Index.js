$(document).ready(function () {
	$('.btn-approve').click(function () {
	    let ma = $('[ma=' + this.id + ']').val();
	  
		let iddiv = this.id;
		btnhide = $('button[ma=' + this.id + ']');
		btnhide.hide();
	
		var varData =
		{
		    ma:ma,
		    id: this.id,
		    appro:1,
		};

		$.ajax({

			type: "POST",

			dataType: "json",

			data: varData,

			url: "/Watch/approve",

			error: function (response) {
			    btnhide.show();
				console.error(response);
			},

			success: function (response) {
				if (response) {

					$('#' + iddiv).addClass('bg-success text-white');
					if ($("#approved").is(':checked'))
					{
						$('.bg-success').hide();
						$('#' + iddiv).hide();
					}
				}
				else {
					alert("مشکلی در سرور رخ داده است");

				}

			},
			complete: function (response) {

			},

		});

	});
	$('.btn-cancel').click(function () {
	    let ma = $('[ma=' + this.id + ']').val();

	    let iddiv = this.id;
	    btnhide = $('button[ma=' + this.id + ']');
	    btnhide.hide();
	    var varData =
		{
		    ma: ma,
		    id: this.id,
		    appro: 2,
		};

	    $.ajax({

	        type: "POST",

	        dataType: "json",

	        data: varData,

	        url: "/Watch/approve",

	        error: function (response) {
	            btnhide.show();
	            console.error(response);
	        },

	        success: function (response) {
	            if (response) {
	               
	                $('#' + iddiv).addClass('bg-danger text-white');
	                if ($("#approved").is(':checked')) {
	                    $('.bg-danger').hide();
	                    $('#' + iddiv).hide();
	                }
	            }
	            else {
	                alert("مشکلی در سرور رخ داده است");

	            }

	        },
	        complete: function (response) {

	        },

	    });

	});
	animation1();
	animation2();
});
function timedRefresh(timeoutPeriod) {
	
	setTimeout(function () {
		location.reload(true);
	
	}, timeoutPeriod);
	window.scrollTo(0, document.body.scrollHeight);

}


function animation1() {
	setInterval(function () { $('.solid').addClass('solidanimation'); }, 12000);
}
function animation2() {
	setInterval(function () { $('.solid').removeClass('solidanimation'); }, 5000);
}


