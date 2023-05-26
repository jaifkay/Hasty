import React from 'react';
import { useParams } from 'react-router-dom';

import Ratings from '../../components/rating/Ratings';
import * as ratingsService from '../../services/ratingsService';

function ListingDetails() {
  const params = useParams();

                <div className="col">
                  <div className="text-end">
                    <Ratings entityId={params.id} entityTypeId={ratingsService.entityTypes.LISTING} />
                  </div>
                </div>
              </Row>

export default ListingDetails;
