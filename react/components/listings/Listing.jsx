import React, { useState } from 'react';
import Ratings from '../rating/Ratings';
import * as ratingsService from '../../services/ratingsService';


function Listing(props) {

          <div className="text-center">
            <Ratings
              isReadOnly={true}
              entityId={listing.id}
              entityTypeId={ratingsService.entityTypes.LISTING}></Ratings>
          </div>
        
export default Listing;
