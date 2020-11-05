var referenceCounter = 1;

function parsePost(text) {
    let code = [...text.matchAll(/```(\w+)[\n|\s](.+?)```\n{0,1}/gmis)];
    for (let x of code) {
        let newCode = document.createElement('div');
        if (/\n/.exec(x[0])) {
            newCode.innerHTML = `<pre class="code"><code class="${x[1]}">${x[2].replaceAll('<', '&lt').replaceAll('>', '&gt')}</code></pre>`;
        }
        else {
            newCode.innerHTML = `<pre class="code inline"><code class="${x[1]}">${x[2].replaceAll('<', '&lt').replaceAll('>', '&gt')}</code></pre>`;
        }
        hljs.highlightBlock(newCode);
        text = text.replace(x[0], newCode.innerHTML);
    }


    let variables = [...text.matchAll(/\@\[(.{0,}?)\]\@/g)];
    let resultText = [];
    let i = 0;
    let index = 0;

    referenceCounter = 1;
    if (typeof window["clearReferences"] === "function") {
        window['clearReferences']();
    }

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
    let text = `<img src="${variable['src']}" alt="${variable['alt']}" "></img>`;
    if ('text' in variable) {
        text += `<div class="img-text">${variable['text']}</div>`;
    }
    return text;
}

function h1Parser(variable) {
    return `<h1 style="margin-left:auto;margin-right:auto;">${variable}</h1>`
}

function referenceParser(variable) {
    if (typeof window["addReference"] === "function" && 'text' in variable) {
        window['addReference']({ 'count': referenceCounter, 'text': variable['text'] });
    }
    return `<sup>[${referenceCounter++}]</sup>`;
    
}