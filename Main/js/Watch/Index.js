$(document).ready(function () {
	$('.btn-approve').click(function () {
		
		let iddiv = this.id;
		let button = this;
		$(button).hide();
		var varData =
		{
			id: this.id,
		};

		$.ajax({

			type: "POST",

			dataType: "json",

			data: varData,

			url: "/Watch/approve",

			error: function (response) {
				$(button).show();
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


