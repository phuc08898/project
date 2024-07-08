import { IBuilderQuery } from '@/interfaces/IBuilderQuery';
import {
  TAtrBuilder, 
  appendQueryList,
  buildQuery,
  removeNullOrEmpty,
  templateQuery,
  toJoin,
  toPaging,
} from '@/utils/queryBuilder';

export const useBuilderQuery = (filter: IBuilderQuery) => {
  let andQuery: TAtrBuilder | string = templateQuery;
  let orQuery: TAtrBuilder | string = templateQuery;

  if (filter.appendQueryAnd) {
    andQuery = filter.appendQueryAnd.reduce(
      (query, item) =>
        appendQueryList(
          query, 
          item.operator, 
          [{
            key: item.key,
            val: item.value
          }]
        ),
        andQuery
    );
  }
  if (filter.appendQueryOr) {
    orQuery = filter.appendQueryOr.reduce(
      (query, item) =>
        appendQueryList(
          query, 
          item.operator, 
          [{
            key: item.key,
            val: item.value
          }]
        ),
      orQuery
    );
  } 
  let newQuery = {
    $and: removeNullOrEmpty(andQuery),
    $or: removeNullOrEmpty(orQuery),
  }
  let requestQuery = buildQuery(newQuery);
 
  requestQuery = filter.toJoin
    ? filter.toJoin.reduce((query, value) => toJoin(query, value), requestQuery)
    : requestQuery;

  const params: string = filter.toPaging
    ? toPaging(filter.toPaging.page, filter.toPaging.pageSize, requestQuery)
    : requestQuery;
    
  return params;
};
