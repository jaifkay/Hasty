import axios from 'axios';
import { onGlobalError, onGlobalSuccess, API_HOST_PREFIX } from './serviceHelpers';
const endpoint = { ratingUrls: `${API_HOST_PREFIX}/api/ratings` };

const addRating = (payload) => {
  const config = {
    method: 'POST',
    url: `${endpoint.ratingUrls}`,
    data: payload,
    crossdomain: true,
    withCredentials: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getRatingSummary = (entityId, entityTypeId) => {
  const config = {
    method: 'GET',
    url: `${endpoint.ratingUrls}/summary?entityId=${entityId}&entityTypeId=${entityTypeId}`,
    withCredentials: false,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const entityTypes = {
  BLOG: 1,
  USER: 2,
  LISTING: 3,
};

export { addRating, getRatingSummary, entityTypes };
