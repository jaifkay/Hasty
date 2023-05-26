import { lazy } from 'react';
const Ratings = lazy(() => import('../components/rating/Ratings'));

const routes = [
  {
    path: '/ratings',
    name: 'Ratings',
    exact: true,
    element: Ratings,
    roles: [],
    isAnonymous: true,
  },  
];

let allRoutes = [...routes, ...errorRoutes];

export default allRoutes;
