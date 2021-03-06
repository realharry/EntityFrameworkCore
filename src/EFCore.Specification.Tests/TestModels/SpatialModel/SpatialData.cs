﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.TestModels.SpatialModel
{
    public class SpatialData : IExpectedData
    {
        private readonly IReadOnlyList<PointEntity> _pointEntities;
        private readonly IReadOnlyList<LineStringEntity> _lineStringEntities;
        private readonly IReadOnlyList<PolygonEntity> _polygonEntities;
        private readonly IReadOnlyList<MultiLineStringEntity> _multiLineStringEntities;

        public SpatialData(IGeometryFactory factory)
        {
            _pointEntities = CreatePointEntities(factory);
            _lineStringEntities = CreateLineStringEntities(factory);
            _polygonEntities = CreatePolygonEntities(factory);
            _multiLineStringEntities = CreateMultiLineStringEntities(factory);
        }

        public virtual IQueryable<TEntity> Set<TEntity>()
            where TEntity : class
        {
            if (typeof(TEntity) == typeof(PointEntity))
            {
                return (IQueryable<TEntity>)_pointEntities.AsQueryable();
            }
            if (typeof(TEntity) == typeof(LineStringEntity))
            {
                return (IQueryable<TEntity>)_lineStringEntities.AsQueryable();
            }
            if (typeof(TEntity) == typeof(PolygonEntity))
            {
                return (IQueryable<TEntity>)_polygonEntities.AsQueryable();
            }
            if (typeof(TEntity) == typeof(MultiLineStringEntity))
            {
                return (IQueryable<TEntity>)_multiLineStringEntities.AsQueryable();
            }

            throw new InvalidOperationException("Unknown entity type: " + typeof(TEntity));
        }

        public static IReadOnlyList<PointEntity> CreatePointEntities(IGeometryFactory factory)
            => new[]
            {
                new PointEntity
                {
                    Id = Guid.Parse("2F39AADE-4D8D-42D2-88CE-775C84AB83B1"),
                    Point = factory.CreatePoint(
                        new Coordinate(0, 0))
                }
            };

        public static IReadOnlyList<LineStringEntity> CreateLineStringEntities(IGeometryFactory factory)
            => new[]
            {
                new LineStringEntity
                {
                    Id = 1,
                    LineString = factory.CreateLineString(
                        new[]
                        {
                            new Coordinate(0, 0),
                            new Coordinate(1, 0)
                        })
                }
            };

        public static IReadOnlyList<PolygonEntity> CreatePolygonEntities(IGeometryFactory factory)
            => new[]
            {
                new PolygonEntity
                {
                    Id = Guid.Parse("2F39AADE-4D8D-42D2-88CE-775C84AB83B1"),
                    Polygon = factory.CreatePolygon(
                        new[]
                        {
                            new Coordinate(0, 0),
                            new Coordinate(0, 1),
                            new Coordinate(1, 0),
                            new Coordinate(0, 0)
                        })
                }
            };

        public static IReadOnlyList<MultiLineStringEntity> CreateMultiLineStringEntities(IGeometryFactory factory)
            => new[]
            {
                new MultiLineStringEntity
                {
                    Id = 1,
                    MultiLineString = factory.CreateMultiLineString(
                        new[]
                        {
                            factory.CreateLineString(
                                new[]
                                {
                                    new Coordinate(0, 0),
                                    new Coordinate(0, 1)
                                }),
                            factory.CreateLineString(
                                new[]
                                {
                                    new Coordinate(1, 0),
                                    new Coordinate(1, 1)
                                })
                        })
                }
            };
    }
}
