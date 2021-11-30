function formatPhone(obj) {
    var numbers = obj.value.replace(/\D/g, ''),
        char = { 0: '(', 3: ') ', 6: '-' };
    obj.value = '';
    for (var i = 0; i < numbers.length; i++) {
        obj.value += (char[i] || '') + numbers[i];
    }
}