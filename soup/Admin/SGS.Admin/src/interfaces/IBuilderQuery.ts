
import { TQueryOperator } from '@/utils/queryBuilder';

export type IBuilderQuery = {
  toPaging?: {
    page: number;
    pageSize: number;
  };
  toJoin?: string[];
  appendQuery?: Record<
    string,
    {
      value: string;
      queryOperator: TQueryOperator;
    }
  >[];
  appendQueryAnd?: IQueryItem[];
  appendQueryOr?: IQueryItem[];
};

export type IQueryItem = {
    key: string;
    value: string;
    operator: TQueryOperator;
}