import React, { useEffect, useState } from 'react';
import Ratings from '../rating/Ratings';
import * as ratingsService from '../../services/ratingsService';


function BlogDetails(props) {

                    <div className="text-end">
                      <Ratings entityId={id} entityTypeId={ratingsService.entityTypes.BLOG} />
                    </div>
                   
export default React.memo(BlogDetails);
