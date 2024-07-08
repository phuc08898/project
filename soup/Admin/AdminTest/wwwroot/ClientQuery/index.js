const {
  toJoin,
  appendQuery,
  toPaging,
  buildQuery,
} = require("./httpQueryBuilder");

const bodAtr = {
  status: "ACTIVE",
  name: "",
};

let atrBuilder = {
  $eq: null,
  $fli: null,
};

atrBuilder = appendQuery(atrBuilder, "$eq", bodAtr);
atrBuilder = appendQuery(atrBuilder, "$fli", bodAtr);
console.log(atrBuilder);
let query = buildQuery(atrBuilder);
query = toJoin(query, "type_documents");
query = toJoin(query, "documents");


query = toPaging(query, 1, 1);
console.debug(query);

let url = "http://localhost:5005";
if (query.length > 1) {
  url += "?" + query;
}

