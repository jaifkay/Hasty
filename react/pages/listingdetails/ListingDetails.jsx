import React, { useState, useEffect, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

import Ratings from '../../components/rating/Ratings';
import * as ratingsService from '../../services/ratingsService';

function ListingDetails(props) {
  const params = useParams();

                <div className="col">
                  <div className="text-end">
                    <Ratings entityId={params.id} entityTypeId={ratingsService.entityTypes.LISTING} />
                  </div>
                </div>
              </Row>

export default ListingDetails;
