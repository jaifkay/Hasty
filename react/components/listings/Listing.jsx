import React from 'react';
import Ratings from '../rating/Ratings';
import * as ratingsService from '../../services/ratingsService';


function Listing() {

          <div className="text-center">
            <Ratings
              isReadOnly={true}
              entityId={listing.id}
              entityTypeId={ratingsService.entityTypes.LISTING}></Ratings>
          </div>
        
export default Listing;
