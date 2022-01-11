var retrieveValue = function (ev) {
    var $this = $(this),
        val = $this.data('value');

    if (val) {
        $this.val(val);
    }
},
hideValue = function (ev) {
    var $this = $(this);

    $this.data('value', $this.val());
    $this.val($this.val().replace(/^\d{3}-\d{2}-/, '***-**-'));
};

$('#ssn').focus(retrieveValue);

$('#ssn').blur(hideValue);


$('#ssn').keyup(function () {
        var val = this.value.replace(/\D/g, '');
    val = val.replace(/^(\d{3})/, '$1-');
    val = val.replace(/-(\d{2})/, '-$1-');
    val = val.replace(/(\d)-(\d{4}).*/, '$1-$2');
    this.value = val;
});

$('#save').click(function () {
    var $this = $(this),
        val = $('#ssn').data('value');

    if (val) {
        $('#ssn').val(val);
    }
});

