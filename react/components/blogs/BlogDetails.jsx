import React from 'react';
import Ratings from '../rating/Ratings';
import * as ratingsService from '../../services/ratingsService';


function BlogDetails() {

                    <div className="text-end">
                      <Ratings entityId={id} entityTypeId={ratingsService.entityTypes.BLOG} />
                    </div>
                   
export default React.memo(BlogDetails);
