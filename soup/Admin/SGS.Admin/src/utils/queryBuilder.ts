
export type IQueryItemRequest = {
  key: string;
  val: string; 
}
export type TQueryType = 
  | '$and'
  | '$or';
export type TQueryOperator =
  | '$eq' // bằng vd: a === b (id === 123)
  | '$neq' // không bằng vd a != b (id != 123)

  | '$gt' // so sánh lớn hơn vd: current time > data time
  | '$gte' //  so sánh lớn hơn hoặc bằng vd: current time >= data time
  | '$lt' // so sánh bé vd: current time < data time
  | '$lte' // so sánh bé hơn hoặc bằng vd: current time =< data time

  // $fli: tìm kiếm theo kiểu contains
  // vd a= ban qui; b = so van ban qui 1 => a co trong b
  | '$fli'
  // $fsw: tìm kiếm theo kiểu bắt đầu
  // vd a= so van; b = so van ban qui 1 => b bat dau tu a
  | '$fsw'
  // $few: tìm kiếm theo kiểu kết thúc
  // vd a= qui 1 ; b = so van ban qui 1 => b ket thuc bang a
  | '$few';

export type TAtrBuilder = {
  [key in TQueryOperator]?: null | IQueryItemRequest[]; // Record<Keys, Type> => Sẽ tạo ra Type obj có các Keys và Value
};

export const templateQuery: TAtrBuilder = {
  $eq: null,
  $neq: null,
  $gt: null,
  $gte: null,
  $lt: null,
  $lte: null,
  $fli: null,
  $fsw: null,
  $few: null,
};

export function removeNullOrEmpty(obj: any) {
  for (const key in obj) {
    if (
      obj[key] === null ||
      obj[key] === '' ||
      (typeof obj[key] === 'object' && Object.keys(obj[key]).length === 0) // empty object
    ) {
      delete obj[key];
    }
  }
  return obj;
}

export const appendQueryList = (
  queryBuilder: TAtrBuilder,
  operator: TQueryOperator,
  query: IQueryItemRequest[]
): TAtrBuilder => ({
  ...queryBuilder,
  [operator]: [
    ...(queryBuilder[operator] || []),
    ...query
  ],
});

export const appendQuery = (
  queryBuilder: TAtrBuilder,
  operator: TQueryOperator,
  query: Record<string, any>
): TAtrBuilder => ({
  ...queryBuilder,
  [operator]: {
    ...(queryBuilder[operator] || {}),
    ...removeNullOrEmpty(query),
  },
});

export const buildQuery = (atrBuilder: any) => {
  const sMaxClean = removeNullOrEmpty(atrBuilder);

  const result = `s=${JSON.stringify(sMaxClean)}`; // json to string => query
  // const result = `${JSON.stringify(sMaxClean)}`; // json to string => query

  return result;
};

export const toJoin = (joinQuery: string, value: string) => {
  if (joinQuery === 's={}') {
    joinQuery = '';
  }
  if (joinQuery.length > 4) {
    joinQuery += '&';
  }
  // console.log(joinQuery, value);
  joinQuery += 'joins=' + value;
  return joinQuery;
};

export const toPaging = (currentPage: number, limit: number = 1, query: string = '') => {
  if (query === 's={}') {
    query = '';
  }
  if (query.length > 4) {
    query += `&`;
  }
  query += `page=${currentPage}&limit=${limit}`;
  return query;
};
