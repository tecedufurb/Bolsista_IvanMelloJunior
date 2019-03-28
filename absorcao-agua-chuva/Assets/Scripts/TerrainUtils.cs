using UnityEngine;

public class TerrainUtils {
	
	private TerrainData terrain;

	public TerrainUtils(TerrainData terrainData) {
		terrain = terrainData;
	}

	public void SetTerrainTexture (TerrainType type) {
        float[, ,] alphaMap = terrain.GetAlphamaps(0, 0, terrain.alphamapWidth, terrain.alphamapHeight);
        
        for (int x = 0; x < terrain.heightmapWidth-1; x++) {
            for (int y = 0; y < terrain.heightmapHeight-1; y++) {
                alphaMap[x, y, 0] = 0;
                alphaMap[x, y, 1] = 0;
                alphaMap[x, y, 2] = 0;
				alphaMap[x, y, (int)type] = 1;
            }
        }

        terrain.SetAlphamaps(0, 0, alphaMap);
	}
}
