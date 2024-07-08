
const templateQuery = {
    $eq: null, // bằng vd: a === b (id === 123)
    $neq: null, // không bằng vd a != b (id != 123)
    $gt: null, // so sánh lớn hơn vd: current time > data time
    $gte: null, //  so sánh lớn hơn hoặc bằng vd: current time >= data time
    $lt: null, // so sánh bé vd: current time < data time
    $lte: null,// so sánh bé hơn hoặc bằng vd: current time =< data time
    ///
    // tìm kiếm theo kiểu contains 
    // vd a= ban qui; b = so van ban qui 1 => a co trong b
    ///
    $fli: null,
    ///
    // tìm kiếm theo kiểu bắt đầu
    // vd a= so van; b = so van ban qui 1 => b bat dau tu a
    ///
    $fsw: null,
    ///
    // tìm kiếm theo kiểu kết thúc
    // vd a= qui 1 ; b = so van ban qui 1 => b ket thuc bang a
    ///
    $few: null,
};



const appendQuery = (atrBuilder, type, values) => {
    const sQ = removeNullOrEmpty(values);
    atrBuilder[type] = sQ;
    return atrBuilder;
}

const buildQuery = (smax) => {
    const sMaxClean = removeNullOrEmpty(smax);
    const result = `s=${JSON.stringify(sMaxClean)}`;


    return result;
}

const toJoin = (joinQuery, value) => {
    if (joinQuery === "s={}") {
        joinQuery = "";
    }

    if (joinQuery.length > 4) {
        joinQuery += "&";
    }

    joinQuery += "joins=" + value;
    return joinQuery;
};


const toPaging = (query, currentPage, limit = 1) => {
    if (query === "s={}") {
        query = "";
    }
    if (query.length > 4) {
        query += `&`;
    }

    query += `page=${currentPage}&limit=${limit}`;
    return query;
}


function removeNullOrEmpty(obj) {
    for (const key in obj) {
        if (obj[key] === null || obj[key] === '') {
            delete obj[key];
        }
    }
    return obj;
}



module.exports = {
    toJoin,
    appendQuery,
    buildQuery,
    toPaging
}