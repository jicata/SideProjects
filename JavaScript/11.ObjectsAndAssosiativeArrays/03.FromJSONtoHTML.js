/*jshint esversion: 6 */
function fromJSONToHTMLTable(input){
    let html = "<table>\n";
    let arr = JSON.parse(input);
    html += " <tr>";
    for (let key of Object.keys(arr[0])){
        html += `<th>${key}</th>`;
    }
    html += "</tr>\n";
    for (let obj of arr) {

        html+="<tr>";
       for(let key of Object.keys(obj)){
           html+=`<td>${obj[key]}</td>`;
       }
       html+="</tr>\n";
    }
    return html + "</table>";


}
console.log(fromJSONToHTMLTable(['[{"Name":"Pesho","Age":20,"City":"Sofia"},{"Name":"Gosho","Age":18,"City":"Plovdiv"},{"Name":"Angel","Age":18,"City":"Veliko Tarnovo"}]']
));