function parsePost(text) {
    let variables = [...text.matchAll(/\@\[(.{0,}?)\]\@/g)];
    let resultText = [];
    let i = 0;
    let index = 0;
    while (i != text.length) {
        if (index != variables.length) {
            if (variables[index].index == i) {
                resultText.push(variables[index][0]);
                i += variables[index][0].length;
                index++;
            }
            else {
                resultText.push(text.substr(i, variables[index].index).replace('\n','<br>'));
                i = variables[index].index;
            }
        }
        else {
            resultText.push(text.substr(i).replace('\n', '<br>'));
            i = text.length;
        }
    }

    return resultText.join('');
}