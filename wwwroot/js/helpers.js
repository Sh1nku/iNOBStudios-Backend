function parseErrors(results) {
    let errors;
    if (results.status == 400) {
        if ('responseText' in results) {
            errors = JSON.parse(results.responseText).errors;
        }
        else {
            errors = results.responseJSON.errors;
        }
        let parsedErrors = []
        for (let i in errors) {
            for (let j in errors[i]) {
                parsedErrors.push(errors[i][j]);
            }
        }
        return parsedErrors;

    }
    else if (results.status == 401 || results.status == 403) {
        return ["Unauthorized"];
    }
    else if (results.status == 404) {
        return ["Not Found"];
    }
    else {
        return ["Unknown error"];
    }
}

function getDateFromJSON(date) {
    return date.slice(0, 10);
}
