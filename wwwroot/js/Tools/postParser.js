function parsePost(text) {
    let variables = [...text.matchAll(/\@\[(.{0,}?)\]\@/g)];
    let resultText = [];
    let i = 0;
    let index = 0;
    while (i != text.length) {
        if (index != variables.length) {
            if (variables[index].index == i) {
                resultText.push(parseJsonText(variables[index][0],variables[index][1]));
                i += variables[index][0].length;
                index++;
            }
            else {
                resultText.push(text.substr(i, variables[index].index-i).replaceAll('\n','<br>'));
                i = variables[index].index;
            }
        }
        else {
            resultText.push(text.substr(i).replaceAll('\n', '<br>'));
            i = text.length;
        }
    }
    return resultText.join('');
}

function parseJsonText(totalText,variable) {
    try {
        variable = JSON.parse(variable);
    }
    catch {
        variable = [...totalText.matchAll(/\@\[(.{0,})\=(.{0,})\]\@/g)]
        if (typeof variable != 'undefined' && typeof window[variable[0][1] + 'Parser'] === "function"){
            window[variable[0][1] + 'Parser'](variable[0][2]);
        }
        return totalText;
        
    }
    if (variable['type'] && typeof window[variable['type'] + 'Parser'] === "function") {
        return window[variable['type'] + 'Parser'](variable);
    }
    return '';
}

function imgParser(variable) {
    return `<img src="${variable['src']}" alt="${variable['alt']}" "></img>`;
}

function h1Parser(variable) {
    return `<h1 style="margin-left:auto;margin-right:auto;">${variable}</h1>`
}